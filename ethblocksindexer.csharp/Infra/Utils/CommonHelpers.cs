using EthBlocksIndexer.Csharp.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Infra.Utils
{
    public static class CommonHelpers
    {
        public static TConfig LoadNameValueConfig<TConfig>()
        {            
            try
            {
                var customSection = (NameValueCollection)ConfigurationManager.GetSection(typeof(TConfig).Name);
                string json = JsonConvert.SerializeObject(customSection.AllKeys.ToDictionary(k => k, k => customSection[k]), new JsonSerializerSettings { });
                return JsonConvert.DeserializeObject<TConfig>(json);
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            return default;
        }
      
    }
}
