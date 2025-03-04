using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Models
{
    public class Watchlist
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public List<string> Stocks { get; set; }

        public Watchlist() { }
        public Watchlist(string name)
        {
            this.Name = name;
        }
    }
}
