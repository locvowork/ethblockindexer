﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Services.Models
{
    public class EthGetTransactionByBlockNumberAndIndexResultModel
    {
        public string blockHash { get; set; }
        public string blockNumber { get; set; }
        public string from { get; set; }
        public string gas { get; set; }
        public string gasPrice { get; set; }
        public string hash { get; set; }
        public string input { get; set; }
        public string nonce { get; set; }
        public string to { get; set; }
        public string transactionIndex { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        public string chainId { get; set; }
        public string v { get; set; }
        public string r { get; set; }
        public string s { get; set; }
    }
}
