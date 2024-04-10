using CatarinaBtg.Models;

namespace CatarinaBtg.Interfaces {
    public interface ITransactionLimitRepository
    {
        Task<TransactionLimit> GetTransactionLimitAsync(string cpf);
        Task AddOrUpdateTransactionLimitAsync(TransactionLimit transactionLimit);
        Task RemoveTransactionLimitAsync(TransactionLimit transactionLimit);
    }
}
