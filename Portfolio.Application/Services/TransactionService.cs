using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateTransactionAsync(TransactionDto dto)
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

        // Map entity to DTO
        return transactions.Select(t => new TransactionDto{
            Symbol = t.Symbol,
            Quantity = t.Quantity,
            PurchasePrice = t.PurchasePrice
        });
    }
}