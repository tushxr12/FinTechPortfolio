public interface IMarketDataService
{
    Task<decimal> GetLivePriceAsync(string symbol);
}