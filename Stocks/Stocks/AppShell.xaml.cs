using Stocks.Models;
using Stocks.Views;
using System.Text.Json;

namespace Stocks
{
    public class StockSearchHandler : SearchHandler
    {
        public static IList<Stock> Stocks { get; set; }

        public Type SelectedItemNavigationTarget { get; set; }      // Used if you need atypical, navigation endpoint - not the default chosen "one"

        public StockSearchHandler(IList<Stock> stocks)
        {
            Stocks = stocks;
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
                ItemsSource = null;
            else
            {
                string query = newValue.ToLower();

                ItemsSource = Stocks
                    .Where(stock => stock.Ticker.ToLower().Contains(query) || stock.Company.ToLower().Contains(query))
                    .ToList<Stock>();
            }
        }
    }

    public partial class AppShell : Shell
    {
        public IList<Stock> Stocks { get; set; }

        public StockSearchHandler StockSearchHandler { get; set; }

        public AppShell()
        {
            InitializeComponent();

            var dataLoader = App.DataLoader;

            Stocks = dataLoader.LoadInMauiJsonAsset(Definitions.STOCKS_JSON).Result;

            StockSearchHandler = new StockSearchHandler(Stocks);

            Routing.RegisterRoute(nameof(Views.SearchPage), typeof(Views.SearchPage));
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
