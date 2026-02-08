using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetPortfolioAsync();
    Task CreateTransactionAsync(TransactionCreateDTO transactionDto);
}