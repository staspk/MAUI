using System.Security.Cryptography;

namespace Stocks.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();

        Shell.SetSearchHandler(this, ((AppShell)Shell.Current).StockSearchHandler);
	}

    protected override void OnAppearing()
    {
        var screenWidth = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width;
        var screenHeight = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height;
        var debug = 10;
    }

    void OnTextChanged(object sender, EventArgs e)
    {
        string URL = "https://finance.yahoo.com/";
    }

    async void Cancel_Tapped(object sender, TappedEventArgs args)
    {
        await Shell.Current.GoToAsync("..");
    }
}