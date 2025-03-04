using Stocks.Models;

namespace Stocks.Views;

public partial class WatchlistsPage : ContentPage
{
    public WatchlistsPage()
    {
        InitializeComponent();

        BindingContext = new Watchlists();
    }

    protected override void OnAppearing()
    {
        //((Models.Watchlists)BindingContext).watchlists.Count


        if (((Models.Watchlists)BindingContext).watchlists.Count < 1)
            WatchlistsContentPage.Title = "Watchlists";
        else
            WatchlistsContentPage.Title = ((Models.Watchlists)BindingContext).watchlists[0].Name;
    }

    private async void Search_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SearchPage));
    }

    private async void AddWatchlist_Clicked(object sender, EventArgs e)
    {
        string newWatchlistName = await DisplayPromptAsync(
            "New Watchlist",
            "Enter the watchlist name:",
            accept: "Add",
            cancel: "Cancel",
            placeholder: ""
        );

        if (!string.IsNullOrEmpty(newWatchlistName))
        {
            foreach (var file in Directory.EnumerateFiles(Definitions.WATCHLISTS_DIR, "*.txt"))
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                if (fileName.Equals(newWatchlistName, StringComparison.OrdinalIgnoreCase))
                {
                    await DisplayAlert("Unable to Create Watchlist", $"{newWatchlistName} already exists!", "OK");
                    return;
                }
            }

            Watchlists.SaveNewWatchlist(new Watchlist(newWatchlistName));

            WATCHLIST_LABEL.Text = newWatchlistName;

            ((Watchlists)BindingContext).CurrentWatchlist.Clear();
        }
    }

    private async void Menu_Tapped(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    //SelectionChanged == clicked on
    private async void CURRENT_WATCHLIST_COLLECTION_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var stock = (Models.Stock)e.CurrentSelection[0];

            await Shell.Current.GoToAsync($"{nameof(StockPage)}?{stock}");

            //currentWatchlistCollection.SelectedItem = null;
        }
    }
}