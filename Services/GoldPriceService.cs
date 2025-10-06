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
        private decimal _cachedGoldPrice = 125.00m; // Güncel fallback değer

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
                _logger.LogInformation($"Gold API response: {response}");

                decimal pricePerOunce = 0;
                try {
                    var goldArray = Newtonsoft.Json.JsonConvert.DeserializeObject<List<decimal>>(response);
                    if (goldArray != null && goldArray.Count > 0)
                        pricePerOunce = goldArray[0];
                } catch {}
                if (pricePerOunce == 0) {
                    try {
                        var goldObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
                        if (goldObj != null && goldObj.gold != null)
                            pricePerOunce = (decimal)goldObj.gold;
                    } catch {}
                }

                if (pricePerOunce > 0)
                {
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
