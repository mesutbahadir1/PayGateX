using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface ITransactionTypeRepository:IRepository<TransactionType>
{
    Task<bool> IsTransactionTypeExist(int id);
}