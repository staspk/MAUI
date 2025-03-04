using Microsoft.Maui.Animations;
using HtmlAgilityPack;
using Stocks.Models;

namespace Stocks.Views;

[QueryProperty(nameof(Ticker), nameof(Ticker))]
public partial class StockPage : ContentPage
{
    private Stock Stock { get; set; }

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

        BindingContext = this;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

    }
    protected async override void OnDisappearing()
    {
        base.OnDisappearing();
    }

    private async Task LoadStock(string ticker)
    {
        Stock = ((AppShell)Shell.Current).Stocks.First(stock => stock.Ticker.ToUpper() == ticker.ToUpper());

        this.TICKER.Text = Stock.Ticker;
        this.COMPANY_NAME.Text = Stock.Company;


        var url = $"https://finance.yahoo.com/quote/{ticker}/";

        try
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true
            };
            client.DefaultRequestHeaders.Add("Pragma", "no-cache");

            string html = await client.GetStringAsync(url);

            float price = float.Parse(ExtractPrice(html).ToString());

            var stats = ExtractStockStats(html);

            float prevClose = float.Parse(stats["Previous Close"]);
            float percentDifference = ((price - prevClose) / prevClose) * 100;


            PRICE.Text = $"{price:F2}";

            DOLLAR_DIFFERENCE.Text = $"{price - prevClose:F2}";
            PERCENT_DIFFERENCE.Text = $"({percentDifference:F2}%)";

            DAYS_RANGE.Text = stats["Day's Range"];
            VOLUME.Text = stats["Volume"];
            MARKET_CAP.Text = stats["Market Cap (intraday)"];

            if(percentDifference < 0)
            {
                PRICE.TextColor = Color.FromArgb("#f44242");
                DOLLAR_DIFFERENCE.TextColor = Color.FromArgb("#f44242");
                PERCENT_DIFFERENCE.TextColor = Color.FromArgb("#f44242");
            }
            else
            {
                PRICE.TextColor = Color.FromArgb("#32a852");
                DOLLAR_DIFFERENCE.TextColor = Color.FromArgb("#32a852");
                PERCENT_DIFFERENCE.TextColor = Color.FromArgb("#32a852");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching stock data for {ticker}: {ex.Message}");
        }
    }
    private Dictionary<string, string> ExtractStockStats(string html)
    {
        var stats = new Dictionary<string, string>();

        // Load the HTML string into an HtmlDocument.
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var statsContainer = doc.DocumentNode.SelectSingleNode("//div[@data-testid='quote-statistics']");
        if (statsContainer != null)
        {
            var liNodes = statsContainer.SelectNodes(".//li");
            if (liNodes != null)
            {
                foreach (var li in liNodes)
                {
                    var labelNode = li.SelectSingleNode(".//span[contains(@class, 'label')]");
                    var valueNode = li.SelectSingleNode(".//span[contains(@class, 'value')]");
                    if (labelNode != null && valueNode != null)
                    {
                        var key = labelNode.InnerText.Trim();
                        var value = valueNode.InnerText.Trim();
                        stats[key] = value;
                    }
                }
            }
        }

        var dateNode = doc.DocumentNode.SelectSingleNode("//span[@data-testid='qsp-price']");
        if (dateNode != null)
        {
            stats["Date"] = dateNode.InnerText.Trim();
        }

        return stats;
    }


    private float? ExtractPrice(string html)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var priceNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@data-testid='qsp-price']");
        if (priceNode != null)
        {
            string priceText = priceNode.InnerText.Trim();

            if (float.TryParse(priceText, out float price))
            {
                return price;
            }
        }
        return null;
    }

    async void AddToWatchlist()
    {

    }

    async void Favorite_Clicked(object sender, EventArgs e)
    {
        string currentAddFavoriteImageFile = ((FileImageSource)ADD_FAVORITE_IMAGE.Source).File;

        if (currentAddFavoriteImageFile == "favorite_add.png")
        {
            ((FileImageSource)ADD_FAVORITE_IMAGE.Source).File = "favorited.png";
        }
        else
        {
            ((FileImageSource)ADD_FAVORITE_IMAGE.Source).File = "favorite_add.png";
        }
    }

    

    async void Search_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SearchPage));
    }

    async void Back_Tapped(object sender, TappedEventArgs args)
    {
        await Shell.Current.GoToAsync("..");
    }
}