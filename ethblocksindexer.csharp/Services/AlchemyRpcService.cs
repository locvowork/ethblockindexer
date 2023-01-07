using EthBlocksIndexer.Csharp.Services.Models;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Services
{
    public class AlchemyRpcService
    {
        private readonly MySqlConnection connection;

        public AlchemyRpcService(MySqlConnection connection)
        {
            this.connection = connection;
        }
        public async Task LoadEthBlocks(int fromBlock, int toBlock)
        {
            await Task.CompletedTask;
            var blockIndexResultMap = new Dictionary<int, AlchemyRpcResult>();
            for(var blockIndex = fromBlock; blockIndex <= toBlock ; blockIndex++)
            {

            }
        }

        
    }
}
