namespace Stocks.Views;

public partial class WatchlistsPage : ContentPage
{
    public WatchlistsPage()
    {
        InitializeComponent();

        BindingContext = new Models.Watchlists();
    }

    protected override void OnAppearing()
    {
        if (((Models.Watchlists)BindingContext).watchlists.Count < 1)
            WatchlistsContentPage.Title = "Watchlists";
        else
            WatchlistsContentPage.Title = ((Models.Watchlists)BindingContext).watchlists[0].Name;
    }

    private async void Search_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SearchPage));
    }

    //SelectionChanged == clicked on
    private async void currentWatchlistCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var stock = (Models.Stock)e.CurrentSelection[0];

            await Shell.Current.GoToAsync($"{nameof(StockPage)}?{stock}");

            //currentWatchlistCollection.SelectedItem = null;
        }
    }
}