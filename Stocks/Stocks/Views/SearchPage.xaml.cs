using Microsoft.Maui.Controls;
using Stocks.Controls;
using Stocks.Models;
using System.Security.Cryptography;

namespace Stocks.Views;

public partial class SearchPage : ContentPage
{

    private IList<Stock> Stocks { get; set; }

    private List<Stock> SearchResultsList  { get; set; }

    public SearchPage()
	{
		InitializeComponent();

        Stocks = ((AppShell)Shell.Current).Stocks;

    }

    protected override void OnAppearing()
    {
        var screenWidth = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width;
        var screenHeight = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height;
        var debug = 10;

    }

    void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;

        string searchStr = e.NewTextValue.ToLower();

        if (searchStr.Length == 0)
            SearchResultsList = new List<Stock>();
        else
        {
            SearchResultsList = Stocks
                        .Where(stock => stock.Ticker.ToLower().StartsWith(searchStr) || stock.Company.ToLower().StartsWith(searchStr))
                        .ToList<Stock>();
        }
            

        string URL = "https://finance.yahoo.com/";
    }

    async void Cancel_Tapped(object sender, TappedEventArgs args)
    {
        await Shell.Current.GoToAsync("..");
    }

    private void OnFavorite_Tapped(object sender, EventArgs e)
    {
        
    }
}