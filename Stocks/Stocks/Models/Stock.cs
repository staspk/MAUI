using System.Text.Json;

namespace Stocks.Models
{
    public class Stock
    {
        public string Ticker { get; set; }
        public string Company { get; set; }
        public float? Price { get; set; }
        public string? ImageFileName { get { return $"{Ticker.ToLower()}.png"; } }

        public Stock() { }
        public Stock(string ticker)
        {
            Ticker = ticker;
        }

        public Stock(string ticker, string company)
        {
            Ticker = ticker;
            Company = company;
        }
    }
}
