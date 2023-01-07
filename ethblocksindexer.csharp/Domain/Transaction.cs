﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthBlocksIndexer.Csharp.Domain
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int BlockId { get; set; }
        public string Hash { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Value { get; set; }
        public decimal Gas { get; set; }
        public decimal GasPrice { get; set; }
        public int TransactionIndex { get; set; }

    }
}
