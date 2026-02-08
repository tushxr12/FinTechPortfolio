using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Portfolio.Application.Interfaces;

namespace Portfolio.Infrastructure.ExternalServices;

public class AlphaVantageService : IMarketDataService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public AlphaVantageService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["AlphaVantage:ApiKey"] ?? throw new Exception("API key missing");
    }

    public async Task<decimal> GetLivePriceAsync(string symbol)
    {
        var url = $"?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_apiKey}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        // Alpha Vantage returns data in "Global Quote" nested object
        if(doc.RootElement.TryGetProperty("Global Quote", out var quote) && quote.TryGetProperty("05. price", out var priceElement))
        {
            return decimal.Parse(priceElement.GetString()!);
        }

        return 0;
    }
}