using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCrafts.Website.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.Website.Services
{
    public class JsonFileProductService
    {
        private IWebHostEnvironment WebHostEnvironment { get; }

        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json");

        public IEnumerable<Product> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Product[]>(
                    jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true}
                );
            }
        }

        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();
            var query = products.First(item => item.Id == productId);

            if (query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions(){SkipValidation = true, Indented = true}),
                    products
                );
            }
        }
    }
}