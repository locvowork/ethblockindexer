using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Infra.Utils
{
    public static class CryptoHelpers
    {
        public static string ToHexString(this int val)
        {
            return val.ToString("X");
        }
    }
}
