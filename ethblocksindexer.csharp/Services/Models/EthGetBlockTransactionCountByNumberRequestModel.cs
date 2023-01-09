using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Services.Models
{
    public class EthGetBlockTransactionCountByNumberRequestModel:JsonRpcRequest
    {
        public EthGetBlockTransactionCountByNumberRequestModel(string blockNumberHex) :base("eth_getBlockTransactionCountByNumber", new List<object> { blockNumberHex })
        {

        }
    }
}
