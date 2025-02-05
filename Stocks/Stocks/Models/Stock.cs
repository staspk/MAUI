using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Models
{
    internal class Stock
    {
        public string Ticker { get; set; }
        public float Price { get; set; }

        public Stock(string Ticker)
        {
            this.Ticker = Ticker;
        }
    }
}
