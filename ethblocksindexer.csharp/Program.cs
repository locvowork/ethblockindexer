using EthBlocksIndexer.Csharp.Infra.Utils;
using EthBlocksIndexer.Csharp.Services.Models;

namespace EthBlocksIndexer.Csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var alchemyConfig = CommonHelpers.LoadNameValueConfig<AlchemyConfig>();
        }
    }
}
