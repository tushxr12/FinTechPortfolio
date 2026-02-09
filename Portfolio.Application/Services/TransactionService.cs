using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    private readonly IMarketDataService _marketDataService;

    public TransactionService(ITransactionRepository repository, IMarketDataService marketDataService)
    {
        _repository = repository;
        _marketDataService = marketDataService;
    }

    public async Task CreateTransactionAsync(TransactionCreateDTO dto)
    {
        if(dto.Quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");
        
        var transaction = new Transaction
        {
            Symbol = dto.Symbol.ToUpper(),
            Quantity = dto.Quantity,
            PurchasePrice = dto.PurchasePrice,
            TransactionDate = DateTime.UtcNow 
        };

        await _repository.AddTransactionAsync(transaction);
    }

    public async Task<IEnumerable<TransactionDto>> GetPortfolioAsync()
    {
        var transactions = await _repository.GetAllTransactionsAsync();
        var dtos = new List<TransactionDto>();

        foreach(var transaction in transactions)
        {
            // Fetch live price from market data service
            var currentPrice = await _marketDataService.GetLivePriceAsync(transaction.Symbol);

            // Perform calculations
            var profitLoss = (currentPrice - transaction.PurchasePrice) * transaction.Quantity;
            var profitLossPercentage = (transaction.PurchasePrice > 0) ? ((currentPrice / (transaction.PurchasePrice) - 1)* 100) : 0;

            dtos.Add(new TransactionDto{
                Id = transaction.Id,
                Symbol = transaction.Symbol,
                Quantity = transaction.Quantity,
                PurchasePrice = Math.Round(transaction.PurchasePrice,2),
                CurrentPrice = Math.Round(currentPrice,2),
                ProfitLoss = Math.Round(profitLoss,2),
                ProfitLossPercentage = Math.Round(profitLossPercentage,2)
            });
        }
        return dtos;
    }
}