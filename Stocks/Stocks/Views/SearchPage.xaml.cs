using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using Stocks.Controls;
using Stocks.Models;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace Stocks.Views;

public partial class SearchPage : ContentPage
{
    
    private IList<Stock> Stocks { get; set; }

    public IList<Stock> SearchResultsList { get; set; }

    public SearchPage()
	{
		InitializeComponent();

        Stocks = ((AppShell)Shell.Current).Stocks;

        BindingContext = this;

        this.Loaded += (s, e) => SearchBar.Focus(); 
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var screenWidth = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width;
        var screenHeight = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height;
        var debug = 10;

    }

    void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;

        string searchStr = e.NewTextValue.ToLower();

        if (searchStr.Length == 0)
            this.SearchResultsList = new List<Stock>();
        else
        {
            this.SearchResultsList = Stocks
                        .Where(stock => stock.Ticker.ToLower().StartsWith(searchStr) || stock.Company.ToLower().StartsWith(searchStr))
                        .ToList<Stock>();
        }

        SEARCH_RESULTS_REGION.Children.Clear();

        foreach (Stock stock in this.SearchResultsList)
        {
            SEARCH_RESULTS_REGION.Children.Add(ConstructRow(stock));
        }
    }

    async void Cancel_Tapped(object sender, TappedEventArgs args)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void TapStock_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Stock tappedStock)
        {
            await Shell.Current.GoToAsync($"{nameof(StockPage)}?Ticker={tappedStock.Ticker}");
        }
    }

    private void TapFavoriteStock_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Stock tappedStock)
        {
            Console.WriteLine($"Favorite tapped for: {tappedStock.Ticker}");
        }
    }

    private FlexLayout ConstructRow(Stock stock)
    {
        var tap_stock = new TapGestureRecognizer();
        tap_stock.Tapped += TapStock_Tapped;
        tap_stock.CommandParameter = stock;

        var flexLayout = new FlexLayout
        {
            Direction = FlexDirection.Row,
            AlignItems = FlexAlignItems.Center,
            Margin = new Thickness(0, 15, 0, 15),
            Padding = new Thickness(12),
            BackgroundColor = Color.FromArgb("#272b32")
        };

        var image = new Image
        {
            Source = stock.ImageFileName,
            Margin = new Thickness(8, 0, 0, 1),
            HeightRequest = 50,
            WidthRequest = 50
        };
        image.GestureRecognizers.Add(tap_stock);

        var label = new Label
        {
            Margin = new Thickness(10, 0, 2, 0),
            Text = stock.Ticker,
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.FloralWhite
        };
        label.GestureRecognizers.Add(tap_stock);

        var spacer = new BoxView
        {
            BackgroundColor = Colors.Transparent
        };
        FlexLayout.SetGrow(spacer, 1);
        spacer.GestureRecognizers.Add(tap_stock);

        var favoriteImage = new Image
        {
            Source = "add_favorite_empty.png",
            Margin = new Thickness(0, 0, 10, 0),
            HeightRequest = 25,
            WidthRequest = 25,
        };
        var tap_favorite_stock = new TapGestureRecognizer();
        tap_favorite_stock.Tapped += TapFavoriteStock_Tapped;
        tap_favorite_stock.CommandParameter = stock;
        favoriteImage.GestureRecognizers.Add(tap_favorite_stock);

        flexLayout.Children.Add(image);
        flexLayout.Children.Add(label);
        flexLayout.Children.Add(spacer);
        flexLayout.Children.Add(favoriteImage);

        return flexLayout;
    }
}