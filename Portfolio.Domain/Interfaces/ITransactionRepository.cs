using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    Task AddTransactionAsync(Transaction transaction);
}