using System;
using System.Collections.Generic;
using System.IO;
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
    }
}