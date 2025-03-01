using Stocks.Models;
using System.Text.Json;

namespace Stocks.Services
{
    public interface IDataLoader
    {
        Task<string> LoadInMauiAsset(string file);
        Task<IList<Stock>> LoadInMauiJsonAsset(string file);

        //Task<IList<Stock>> LoadInStocks(string file);
    }

    public class DataLoader : IDataLoader
    {

        public async Task<IList<Stock>> LoadInMauiJsonAsset(string file)  // file == stocks.json
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(file);

                return await JsonSerializer.DeserializeAsync<IList<Stock>>(stream);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error parsing json: {ex.Message}");
                throw ex;
            }
        }

        public async Task<string> LoadInMauiAsset(string file)
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(file);
                using var reader = new StreamReader(stream);

                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public void ProcessData(string data)
        {
            Console.WriteLine($"Data loaded: {data.Substring(0, Math.Min(100, data.Length))}...");
        }
    }
}