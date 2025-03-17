using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Models
{
    public class WatchlistsViewModel : INotifyPropertyChanged
    {
        public List<Watchlist> Watchlists { get; set; }
        public ObservableCollection<Stock> CurrentWatchlist { get; set; } = new ObservableCollection<Stock>();

        private IList<Stock> Stocks { get; set; }

        private Watchlist _selectedWatchlist;
        public Watchlist SelectedWatchlist
        {
            get => _selectedWatchlist;
            set
            {
                if (_selectedWatchlist != value)
                {
                    _selectedWatchlist = value;
                    OnPropertyChanged();
                    LoadWatchlist(_selectedWatchlist);
                }
            }
        }

        public WatchlistsViewModel(IList<Stock> stocks)
        {
            Stocks = stocks;
            this.Watchlists = LoadSavedWatchlists();

            


            if(this.Watchlists.Count > 0)
                LoadWatchlist(this.Watchlists[0]);
        }

        

        private void LoadWatchlist(Watchlist watchlist = null)
        {
            CurrentWatchlist.Clear();

            if (watchlist is null && Watchlists.Count > 0)
            {
                watchlist = this.Watchlists[0];

                foreach (var stockStr in watchlist.Stocks)
                    CurrentWatchlist.Add(Stocks.First(stock => stock.Ticker.ToUpper() == stockStr.ToUpper()));
            }
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

                //SelectedWatchlist = 
            }

            return watchlists.OrderBy(watchlist => watchlist.Order).ToList();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
