using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks
{
    public static class Definitions
    {
        public static readonly string APP_DATA = Path.Combine(FileSystem.AppDataDirectory, "app-data");

        public static readonly string STOCKS_JSON = "Stocks.json";

        public static readonly string WATCHLISTS_DIR = Path.Combine(FileSystem.AppDataDirectory, "Watchlists");
    }
}
