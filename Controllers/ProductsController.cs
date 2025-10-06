using Microsoft.AspNetCore.Mvc;
using Renart.Models;
using Renart.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace Renart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGoldPriceService _goldPriceService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IGoldPriceService goldPriceService, ILogger<ProductsController> logger, IWebHostEnvironment environment)
        {
            _goldPriceService = goldPriceService;
            _logger = logger;
            _environment = environment;
        }

        [HttpGet]
        [EnableCors("ReactApp")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] decimal? minPopularity = null)
        {
            try
            {
                var products = await LoadProductsAsync();
                var goldPrice = await _goldPriceService.GetGoldPricePerGramAsync();

                foreach (var product in products)
                {
                    product.Price = (product.PopularityScore + 1) * product.Weight * goldPrice;
                }

                var filteredProducts = products.AsQueryable();

                if (minPrice.HasValue)
                    filteredProducts = filteredProducts.Where(p => p.Price >= minPrice.Value);

                if (maxPrice.HasValue)
                    filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice.Value);

                if (minPopularity.HasValue)
                    filteredProducts = filteredProducts.Where(p => p.PopularityScore >= minPopularity.Value);

                return Ok(filteredProducts.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products");
                return StatusCode(500, "An error occurred while retrieving products");
            }
        }

        private async Task<List<Product>> LoadProductsAsync()
        {
            var filePath = Path.Combine(_environment.WebRootPath ?? _environment.ContentRootPath, "wwwroot", "data", "products.json");
            
            if (!System.IO.File.Exists(filePath))
            {
                return GetSampleProducts();
            }

            try
            {
                var json = await System.IO.File.ReadAllTextAsync(filePath);
                return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading products from file");
                return GetSampleProducts();
            }
        }

        private static List<Product> GetSampleProducts() // Fallback sample data
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Engagement Ring 1",
                    PopularityScore = 0.85m,
                    Weight = 2.1m,
                    Images = new ProductImages
                    {
                        Yellow = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG085-100P-Y.jpg?v=1696588368",
                        Rose = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG085-100P-R.jpg?v=1696588406",
                        White = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG085-100P-W.jpg?v=1696588402"
                    }
                },
                new Product
                {
                    Id = 2,
                    Name = "Engagement Ring 2",
                    PopularityScore = 0.51m,
                    Weight = 3.4m,
                    Images = new ProductImages
                    {
                        Yellow = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG012-Y.jpg?v=1707727068",
                        Rose = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG012-R.jpg?v=1707727068",
                        White = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG012-W.jpg?v=1707727068"
                    }
                },
                new Product
                {
                    Id = 3,
                    Name = "Engagement Ring 3",
                    PopularityScore = 0.92m,
                    Weight = 3.8m,
                    Images = new ProductImages
                    {
                        Yellow = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG020-100P-Y.jpg?v=1683534032",
                        Rose = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG020-100P-R.jpg?v=1683534032",
                        White = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG020-100P-W.jpg?v=1683534032"
                    }
                },
                new Product
                {
                    Id = 4,
                    Name = "Engagement Ring 4",
                    PopularityScore = 0.88m,
                    Weight = 4.5m,
                    Images = new ProductImages
                    {
                        Yellow = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG022-100P-Y.jpg?v=1683532153",
                        Rose = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG022-100P-R.jpg?v=1683532153",
                        White = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG022-100P-W.jpg?v=1683532153"
                    }
                },
                new Product
                {
                    Id = 5,
                    Name = "Engagement Ring 5",
                    PopularityScore = 0.80m,
                    Weight = 2.5m,
                    Images = new ProductImages
                    {
                        Yellow = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG074-100P-Y.jpg?v=1696232035",
                        Rose = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG074-100P-R.jpg?v=1696927124",
                        White = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG074-100P-W.jpg?v=1696927124"
                    }
                },
                new Product
                {
                    Id = 6,
                    Name = "Engagement Ring 6",
                    PopularityScore = 0.82m,
                    Weight = 1.8m,
                    Images = new ProductImages
                    {
                        Yellow = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG075-100P-Y.jpg?v=1696591786",
                        Rose = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG075-100P-R.jpg?v=1696591802",
                        White = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG075-100P-W.jpg?v=1696591798"
                    }
                },
                new Product
                {
                    Id = 7,
                    Name = "Engagement Ring 7",
                    PopularityScore = 0.70m,
                    Weight = 5.2m,
                    Images = new ProductImages
                    {
                        Yellow = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG094-100P-Y.jpg?v=1696589183",
                        Rose = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG094-100P-R.jpg?v=1696589214",
                        White = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG094-100P-W.jpg?v=1696589210"
                    }
                },
                new Product
                {
                    Id = 8,
                    Name = "Engagement Ring 8",
                    PopularityScore = 0.90m,
                    Weight = 3.7m,
                    Images = new ProductImages
                    {
                        Yellow = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG115-100P-Y.jpg?v=1696596076",
                        Rose = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG115-100P-R.jpg?v=1696596151",
                        White = "https://cdn.shopify.com/s/files/1/0484/1429/4167/files/EG115-100P-W.jpg?v=1696596147"
                    }
                }
            };
        }
    }
}