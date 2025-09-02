using PayGateX.Dtos.Transaction;
using PayGateX.Entities;

namespace PayGateX.Service.Contracts;

public interface ITransactionService
{
    Task<List<Transaction>> GetAllTransaction();
    
    Task<Transaction> GetTransactionById(int id);
    
    Task<Transaction> CreateTransaction(int cardId, int transactionTypeId, int paymentMethodTypeId,
       int currencyId,CreateTransactionDto createTransactionDto,string userName);
    
    Task<Transaction> UpdateTransaction(int id, Transaction transaction);
    
    Task<Transaction> DeleteTransaction(int id);
}