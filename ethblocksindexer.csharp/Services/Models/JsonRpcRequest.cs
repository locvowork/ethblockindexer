using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Services.Models
{
    public class JsonRpcRequest
    {
        public JsonRpcRequest(string method, List<object> @params, object id = null, string jsonRpc = "2.0")
        {
            JsonRpc = jsonRpc;
            Method = method;
            Params = @params;
            Id = id;
        }

        public string JsonRpc { get; set; }
        public string Method { get; set; }
        public List<object> Params { get; set; }
        public object Id { get; set; }
        
    }
}
