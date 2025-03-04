using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Models
{
    internal class Watchlists
    {
        public List<Watchlist> watchlists { get; set; }
        public ObservableCollection<Stock> CurrentWatchlist { get; set; } = new ObservableCollection<Stock>();

        public Watchlists()
        {
            this.watchlists = LoadSavedWatchlists();

            if(this.watchlists.Count > 0)
                LoadWatchlist(this.watchlists[0]);
        }

        private void LoadWatchlist(Watchlist watchlist = null)
        {
            CurrentWatchlist.Clear();

            var stocks = ((AppShell)Shell.Current).Stocks;

            if (watchlist is null && watchlists.Count > 0)
                watchlist = this.watchlists[0];

            foreach (var stockStr in watchlist.Stocks)
                CurrentWatchlist.Add(stocks.First(stock => stock.Ticker.ToUpper() == stockStr.ToUpper()));
        }

        public static void SaveNewWatchlist(Watchlist watchlist)
        {
            if (!Directory.Exists(Definitions.WATCHLISTS_DIR))
                Directory.CreateDirectory(Definitions.WATCHLISTS_DIR);

            int order = 0;
            foreach (var file in Directory.EnumerateFiles(Definitions.WATCHLISTS_DIR, "*.txt"))
            {
                var firstLine = File.ReadLines(file).FirstOrDefault();
                if (order == int.Parse(firstLine))
                {
                    order++;
                }
            }

            watchlist.Order = order;

            File.WriteAllText(
                Path.Combine(Definitions.WATCHLISTS_DIR, $"{watchlist.Name}.txt"),
                watchlist.Order.ToString()
            );


        }

        public static List<Watchlist> LoadSavedWatchlists()
        {
            List<Watchlist> watchlists = new List<Watchlist>();

            if (!Directory.Exists(Definitions.WATCHLISTS_DIR))
                Directory.CreateDirectory(Definitions.WATCHLISTS_DIR);

            foreach (var file in Directory.EnumerateFiles(Definitions.WATCHLISTS_DIR, "*.txt"))
            {
                var lines = File.ReadAllLines(file).ToList();
                watchlists.Add(new Watchlist()
                {
                    Name = Path.GetFileNameWithoutExtension(file),
                    Order = int.Parse(lines[0]),
                    Stocks = lines.GetRange(1, lines.Count - 1)
                });
            }

            return watchlists.OrderBy(watchlist => watchlist.Order).ToList();
        }
    }
}
