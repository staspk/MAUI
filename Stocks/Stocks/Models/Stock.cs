using System.Text.Json;

namespace Stocks.Models
{
    public class Stock
    {
        public string Ticker { get; set; }
        public string Company { get; set; }
        public float? Price { get; set; }
        public string? ImageFileName { get; set; }

        public Stock(string ticker, string company)
        {
            Ticker = ticker;
            Company = company;
        }

        //public Dictionary<string, Stock> ConstructStockDictionary(JsonDocument json)
        //{
        //    Dictionary<string, Stock> stockDict = new Dictionary<string, Stock>();

        //    using(var stockJson )
        //}
    }
}
