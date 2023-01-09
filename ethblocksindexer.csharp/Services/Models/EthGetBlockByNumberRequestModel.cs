using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Services.Models
{
    public class EthGetBlockByNumberRequestModel : JsonRpcRequest
    {
        public EthGetBlockByNumberRequestModel(string blockNumberHex, int Id, bool includeTransactions = false) : base("eth_getBlockByNumber", new List<object> { blockNumberHex, includeTransactions },Id)
        {

        }
    }
}
