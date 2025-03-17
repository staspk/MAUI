using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Models
{
    public class AppData
    {
        public string LastWatchlistOpen { get; private set; }

        private AppData() { }

        public static AppData LoadAppDataFile()
        {
            ClearAppData();

            var appData = new AppData();

            if (!File.Exists(Definitions.APP_DATA))
            {
                using (var writer = new StreamWriter(Definitions.APP_DATA))
                {
                    writer.WriteLine(appData.LastWatchlistOpen);
                }
            }


            using (StreamReader sr = new StreamReader(Definitions.APP_DATA))
            {
                string line;

                for (int lineNumber = 1; (line = sr.ReadLine()) != null; lineNumber++)
                {
                    if (lineNumber == 1)
                        appData.LastWatchlistOpen = line;
                }
            }

            throw new NotImplementedException();
        }
    
        public static void ClearAppData()
        {
            if (File.Exists(Definitions.APP_DATA))
                File.Delete(Definitions.APP_DATA);
        }

    }
}
