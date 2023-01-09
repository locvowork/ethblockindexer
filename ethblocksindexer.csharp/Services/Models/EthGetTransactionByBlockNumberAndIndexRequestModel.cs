using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Services.Models
{
    public class EthGetTransactionByBlockNumberAndIndexRequestModel: JsonRpcRequest
    {
        public EthGetTransactionByBlockNumberAndIndexRequestModel(string blockNumberHex, string indexHex) :base("eth_getTransactionByBlockNumberAndIndex", new List<object> { blockNumberHex, indexHex})
        {

        }
    }
}
