using Stocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Controls
{
    public class StockSearchHandler : SearchHandler
    {
        public IList<Stock> Stocks { get; set; }

        public Type SelectedItemNavigationTarget { get; set; }      // Used if you need atypical, navigation endpoint - not the default chosen "one"

        public StockSearchHandler() { }

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

        protected override void OnQueryConfirmed()
        {
            base.OnQueryConfirmed();


        }
    }
}
