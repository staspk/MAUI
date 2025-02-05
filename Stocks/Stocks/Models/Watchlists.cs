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
        private static readonly string WATCHLISTS_DIR = Path.Combine(FileSystem.AppDataDirectory, "Watchlists");
        public List<Watchlist> watchlists { get; set; }
        public ObservableCollection<Stock> CurrentWatchlist { get; set; } = new ObservableCollection<Stock>();

        public Watchlists()
        {
            this.watchlists = LoadSavedWatchlists();
            LoadCurrentWatchlist();
        }

        private void LoadCurrentWatchlist()
        {
            CurrentWatchlist.Clear();

            Watchlists.LoadSavedWatchlists();
            if (watchlists.Count > 0)
            {
                foreach (string stock in watchlists[0].Stocks)
                {
                    CurrentWatchlist.Add(new Stock(stock));
                }
            }
        }

        public static List<Watchlist> LoadSavedWatchlists()
        {
            List<Watchlist> watchlists = new List<Watchlist>();

            if (!Directory.Exists(WATCHLISTS_DIR))
                Directory.CreateDirectory(WATCHLISTS_DIR);

            foreach (var file in Directory.EnumerateFiles(WATCHLISTS_DIR, "*.txt"))
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
