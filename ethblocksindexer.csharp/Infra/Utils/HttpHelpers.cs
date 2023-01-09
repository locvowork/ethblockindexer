using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Infra.Utils
{
    public static class HttpHelpers
    {
        public static async Task<string> ExecPost(IHttpClientFactory httpClientFactory,string endpoint,string jsonBody)
        {
            using (var httpClient = httpClientFactory.CreateClient())
            {
                var jsonData = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(endpoint,jsonData);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
