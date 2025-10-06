namespace Renart.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal PopularityScore { get; set; }
        public decimal Weight { get; set; }
        public ProductImages Images { get; set; } = new();
        public decimal Price { get; set; }
        public decimal PopularityRating => PopularityScore * 5;
    }

    public class ProductImages
    {
        public string Yellow { get; set; } = string.Empty;
        public string Rose { get; set; } = string.Empty;
        public string White { get; set; } = string.Empty;
    }
}