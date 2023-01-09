using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Services.Models
{
    public class JsonRpcResponse
    {
        public static TResult TryGetResult<TResult>(string rpcResult)
        {
            var responseData = JsonConvert.DeserializeObject<JsonRpcResponse<TResult>>(rpcResult);
            return responseData.Result;
        }
    }
    public class JsonRpcResponse<TResult>
    {
        public string JsonRpc { get; set; }
        public object Id { get; set; }
        public TResult Result { get; set; }
    }
}
