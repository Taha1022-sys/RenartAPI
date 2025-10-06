namespace Renart.Services
{
    public interface IGoldPriceService
    {
        Task<decimal> GetGoldPricePerGramAsync();
    }

    public class GoldPriceService : IGoldPriceService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GoldPriceService> _logger;
        private decimal _cachedGoldPrice = 75.00m; 

        public GoldPriceService(HttpClient httpClient, ILogger<GoldPriceService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<decimal> GetGoldPricePerGramAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://api.metals.live/v1/spot/gold");
                var goldData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
                
                if (goldData?.price != null)
                {
                    decimal pricePerOunce = goldData.price;
                    _cachedGoldPrice = pricePerOunce / 31.1035m; 
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to fetch gold price, using cached value");
            }

            return _cachedGoldPrice;
        }
    }
}