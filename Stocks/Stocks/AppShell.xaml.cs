using Stocks.Controls;
using Stocks.Models;
using Stocks.Views;
using System.Text.Json;

namespace Stocks
{

    public partial class AppShell : Shell
    {
        public WatchlistsViewModel Watchlists { get; private set; }
        public IList<Stock> Stocks { get; private set; }

        public AppData AppData { get; private set; }

        public AppShell()       // ((AppShell)App.Current)
        {
            InitializeComponent();

            AppData = AppData.LoadAppDataFile();

            Stocks = App.DataLoader.LoadInMauiJsonAsset(Definitions.STOCKS_JSON).Result;

            Watchlists = new WatchlistsViewModel(Stocks);

            BindingContext = Watchlists;

            Routing.RegisterRoute(nameof(Views.WatchlistsPage), typeof(Views.WatchlistsPage));
            Routing.RegisterRoute(nameof(Views.SearchPage), typeof(Views.SearchPage));
            Routing.RegisterRoute(nameof(Views.StockPage), typeof(Views.StockPage));
        }

        protected async override void OnAppearing()
        {
            Console.WriteLine("Hey");
        }



        public async void Search_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SearchPage));
        }

        
    }
}
