using Microsoft.Maui.Animations;

namespace Stocks.Views;

[QueryProperty(nameof(Ticker), nameof(Ticker))]
public partial class StockPage : ContentPage
{
    public string Ticker
    {
        set
        {
            LoadStock(value);
        }
    }
    public StockPage()
    {
        InitializeComponent();
    }

    private void LoadStock(string ticker)
    {

    }
}