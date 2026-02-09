using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Portfolio.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Portfolio.Infrastructure.ExternalServices;

public class AlphaVantageService : IMarketDataService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly IMemoryCache _cache;

    public AlphaVantageService(HttpClient httpClient, IConfiguration config, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _apiKey = config["AlphaVantage:ApiKey"] ?? throw new Exception("API key missing");
        _cache = cache;
    }

    public async Task<decimal> GetLivePriceAsync(string symbol)
    {
        string cacheKey = $"Price_{symbol.ToUpper()}";

        // Check if price exists in cache
        if(_cache.TryGetValue(cacheKey, out decimal cachedPrice))
        {
            Console.WriteLine("Returning cached price");
            return cachedPrice;
        }

        // If not in cache, make the api call
        var url = $"?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_apiKey}";

        var response = await _httpClient.GetAsync(url);
        
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        // Console.WriteLine(json);
        using var doc = JsonDocument.Parse(json);

        // Alpha Vantage returns data in "Global Quote" nested object
        if(doc.RootElement.TryGetProperty("Global Quote", out var quote) && quote.TryGetProperty("05. price", out var priceElement))
        {
            var price = decimal.Parse(priceElement.GetString()!);

            if(price > 0)
            {
                Console.WriteLine("Making external call and store cached price");
                // Store the price in cache for 5 minutes
                _cache.Set(cacheKey, price, TimeSpan.FromMinutes(5));
            }

            return price;
        }

        return 0;
    }
}