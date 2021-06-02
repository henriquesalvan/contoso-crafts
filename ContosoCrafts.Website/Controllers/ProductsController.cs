using System.Collections.Generic;
using ContosoCrafts.Website.Models;
using ContosoCrafts.Website.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.Website.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public JsonFileProductService ProductService { get; }
        
        public ProductsController(JsonFileProductService productService)
        {
            this.ProductService = productService;
        }
        
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return this.ProductService.GetProducts();
        }
    }
}