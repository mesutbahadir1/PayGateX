using PayGateX.Entities;
using PayGateX.Interfaces;
using PayGateX.Service.Contracts;

namespace PayGateX.Service;

public class TransactionTypeService:ITransactionTypeService
{
    private readonly ITransactionTypeRepository _transactionTypeRepository;
    public TransactionTypeService(ITransactionTypeRepository transactionTypeRepository)
    {
        _transactionTypeRepository = transactionTypeRepository;
    }
    public async Task<List<TransactionType>> GetAll()
    {
        var transactionTypes = await _transactionTypeRepository.GetAll();;
        return transactionTypes;
    }

    public async Task<TransactionType> GetById(int id)
    {
        var transactionType = await _transactionTypeRepository.GetById(id);
        return transactionType;
    }

    public async Task<TransactionType> Create(TransactionType entity)
    {
        var createTransactionType = await _transactionTypeRepository.Create(entity);
        return createTransactionType;
    }

    public async Task<TransactionType> Update(int id, TransactionType entity)
    {
        var transactionType = await _transactionTypeRepository.Update(id, entity);
        return transactionType;
    }

    public async Task<TransactionType> Delete(int id)
    {
        var deletedTransactionType = await _transactionTypeRepository.Delete(id);
        return deletedTransactionType;
    }
}