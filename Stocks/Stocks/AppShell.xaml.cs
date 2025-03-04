using Stocks.Controls;
using Stocks.Models;
using Stocks.Views;
using System.Text.Json;

namespace Stocks
{

    public partial class AppShell : Shell
    {
        public IList<Stock> Stocks { get; set; }

        public AppShell()       //((AppShell)App.Current)
        {
            InitializeComponent();

            Stocks = App.DataLoader.LoadInMauiJsonAsset(Definitions.STOCKS_JSON).Result;

            BindingContext = this;

            Routing.RegisterRoute(nameof(Views.WatchlistsPage), typeof(Views.WatchlistsPage));
            Routing.RegisterRoute(nameof(Views.SearchPage), typeof(Views.SearchPage));
            Routing.RegisterRoute(nameof(Views.StockPage), typeof(Views.StockPage));
        }

        protected async override void OnAppearing()
        {
            
        }



        public async void Search_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SearchPage));
        }

        
    }
}
