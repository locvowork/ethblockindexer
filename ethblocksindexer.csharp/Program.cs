using EthBlocksIndexer.Csharp.Infra.Utils;
using EthBlocksIndexer.Csharp.Services;
using EthBlocksIndexer.Csharp.Services.Models;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var alchemyConfig = CommonHelpers.LoadNameValueConfig<AlchemyConfig>();
            var conStr = ConfigurationManager.ConnectionStrings["EthBlocksDbContext"].ConnectionString;
            using (IDbConnection connection = new MySql.Data.MySqlClient.MySqlConnection(conStr))
            {
                var alchemyService = new AlchemyRpcService(connection, alchemyConfig);
                await alchemyService.LoadEthBlocks(12100001, 12100500);
            }
        }
    }
}
