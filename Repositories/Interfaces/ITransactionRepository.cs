using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetAllTransaction();
    
    Task<Transaction> GetTransactionById(int id);
    
    Task<Transaction> CreateTransaction(Transaction transaction);
    
    Task<Transaction> UpdateTransaction(int id, Transaction transaction);
    
    Task<Transaction> DeleteTransaction(int id);
}