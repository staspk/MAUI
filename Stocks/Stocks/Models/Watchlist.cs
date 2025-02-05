using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Models
{
    internal class Watchlist
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public List<string> Stocks { get; set; }
    }
}
