using Dapper;
using EthBlocksIndexer.Csharp.Infra.Utils;
using EthBlocksIndexer.Csharp.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EthBlocksIndexer.Csharp.Services
{
    public class AlchemyRpcService
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IDbConnection connection;
        private readonly AlchemyConfig alchemyConfig;

        public AlchemyRpcService(IDbConnection connection,AlchemyConfig alchemyConfig)
        {
            this.connection = connection;            
            this.alchemyConfig = alchemyConfig;
        }
        public async Task LoadEthBlocks(int fromBlock, int toBlock)
        {
            await Task.CompletedTask;            
            var blockIndexResultMap = new ConcurrentDictionary<int, AlchemyRpcResult>();
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var alchemyEndpoint = alchemyConfig.Https;
            int exceptionTotal = 0;
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            connection.Open();
            var dbTransaction = connection.BeginTransaction();
            for (var blockIndex = fromBlock; blockIndex <= toBlock; blockIndex++)
            {
                if (exceptionTotal < 10)
                {
                    var reqId = blockIndex - fromBlock + 1;
                    var blockNumberHex = new HexBigInteger(blockIndex).HexValue;
                    for (var tryCount = 1; tryCount <= 2; tryCount++)
                    {
                        try
                        {
                            var json = JsonConvert.SerializeObject(new EthGetBlockByNumberRequestModel(blockNumberHex, reqId), serializerSettings);
                            var jsonResponse = await HttpHelpers.ExecPost(httpClientFactory, alchemyEndpoint, json);
                            if (!string.IsNullOrEmpty(jsonResponse))
                            {
                                var responseData = JsonRpcResponse.TryGetResult<EthGetBlockByNumberResultModel>(jsonResponse);
                                if (responseData != null)
                                {
                                    // insert block record to database
                                    var newBlock = await InsertEthBlock(responseData);
                                    _logger.Debug($"Inserted block {newBlock.BlockNumber}");
                                    json = JsonConvert.SerializeObject(new EthGetBlockTransactionCountByNumberRequestModel(blockNumberHex), serializerSettings);
                                    jsonResponse = await HttpHelpers.ExecPost(httpClientFactory, alchemyEndpoint, json);
                                    if (!string.IsNullOrEmpty(jsonResponse))
                                    {
                                        var transactionCount = ((int)new HexBigInteger(JsonRpcResponse.TryGetResult<string>(jsonResponse)).Value);
                                        if (transactionCount > 0)
                                        {
                                            for (var tranIdx = 0; tranIdx < transactionCount; tranIdx++)
                                            {
                                                json = JsonConvert.SerializeObject(new EthGetTransactionByBlockNumberAndIndexRequestModel(blockNumberHex, new HexBigInteger(tranIdx).HexValue), serializerSettings);
                                                jsonResponse = await HttpHelpers.ExecPost(httpClientFactory, alchemyEndpoint, json);
                                                var trasnsactionResult = JsonRpcResponse.TryGetResult<EthGetTransactionByBlockNumberAndIndexResultModel>(jsonResponse);
                                                // insert transaction record to database
                                                try
                                                {
                                                    var newTransaction = await InsertEthBlockTransactions(blockIndex, trasnsactionResult);
                                                    _logger.Debug($"Inserted transaction {newTransaction.Hash} of block {newBlock.BlockNumber}");
                                                }
                                                catch (Exception ex)
                                                {
                                                    _logger.Error(ex);
                                                    throw;
                                                }                                                
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Interlocked.Increment(ref exceptionTotal);
                            _logger.Error(ex);
                            if (exceptionTotal >= 10)
                            {
                                _logger.Debug("Reached 10 exceptions, stop processing to figure out issues");
                                dbTransaction.Rollback();
                                return;
                            }
                            await Task.Delay(500);
                            _logger.Debug($"Retry number {tryCount} for block {blockIndex}");
                        }
                    }
                }
            }            
            dbTransaction.Commit();            
            connection.Close();
            dbTransaction.Dispose();
            connection.Dispose();
        }

        private async Task<Domain.Block> InsertEthBlock(EthGetBlockByNumberResultModel resultModel, IDbTransaction dbTransaction = null)
        {
            var blockNumber = (int)new HexBigInteger(resultModel.number).Value;
            var strSql = "insert into Blocks(BlockNumber, Hash, ParentHash, Miner, BlockReward, GasLimit, GasUsed) ";
            strSql += "Select * From ( Select @BlockNumber, @Hash, @ParentHash, @Miner, @BlockReward, @GasLimit, @GasUsed) as tmp ";
            strSql += " WHERE NOT EXISTS (SELECT BlockNumber FROM Blocks WHERE BlockNumber=@BlockNumber) limit 1;";
            var newBlock = new Domain.Block
            {
                BlockNumber = blockNumber,
                Hash = resultModel.hash,
                ParentHash = resultModel.parentHash,
                GasLimit = (decimal)new HexBigInteger(resultModel.gasLimit).Value,
                GasUsed = (decimal)new HexBigInteger(resultModel.gasUsed).Value,
                Miner = resultModel.miner,
                BlockReward = (decimal)Web3.Convert.ToWei(2, Nethereum.Util.UnitConversion.EthUnit.Ether) // Block reward: For blocks 7,280,000 onward the reward is 2 ETH, and was last set by EIP-1234 at the Constantinople fork. 
            };
            await connection.ExecuteAsync(strSql,newBlock , dbTransaction);
            return newBlock;
        }
        private async Task<Domain.Transaction> InsertEthBlockTransactions(int blockId, EthGetTransactionByBlockNumberAndIndexResultModel resultModel, IDbTransaction dbTransaction = null)
        {
            var transactionIndex = (int)new HexBigInteger(resultModel.transactionIndex).Value;
            var strSql = "insert into Transactions(BlockId, `Hash`, `From`, `To`, `Value`, Gas, GasPrice, TransactionIndex) ";
            strSql += "Select * from( Select @BlockId, @Hash, @From, @To, @Value, @Gas, @GasPrice, @TransactionIndex) as tmp";
            strSql += " WHERE NOT EXISTS (SELECT BlockId FROM Transactions WHERE BlockId=@BlockId and TransactionIndex = @TransactionIndex) limit 1;";
            var newTransaction = new Domain.Transaction
            {
                BlockId = blockId,
                Hash = resultModel.hash,
                From = resultModel.from,
                To = resultModel.to,
                Value = (decimal)new HexBigInteger(resultModel.value).Value,
                Gas = (decimal)new HexBigInteger(resultModel.gas).Value,
                GasPrice = (decimal)new HexBigInteger(resultModel.gasPrice).Value,
                TransactionIndex = (int)new HexBigInteger(resultModel.transactionIndex).Value

            };
            await connection.ExecuteAsync(strSql, newTransaction, dbTransaction );
            return newTransaction;
        }
    }
}
