using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Services.Models
{
    public class AlchemyConfig
    {
        public string ApiKey { get; set; }
        public string Https { get; set; }
        public string WebSockets { get; set; }

    }
}
