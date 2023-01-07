using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.Client.RpcMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Services.Models
{
    public class AlchemyRpcResult
    {
        public RpcRequest Request { get; set; }
        public RpcResponseMessage Response { get; set; }
        public int BlockNumber { get; set; }
        public string BlockNumberHex { get; set; }
        public bool IsSuccess { get; set; }
    }
}
