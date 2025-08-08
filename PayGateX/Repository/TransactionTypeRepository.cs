using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class TransactionTypeRepository:ITransactionTypeRepository
{
    private readonly ApplicationDBContext _context;
    public TransactionTypeRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<TransactionType>> GetAll()
    {
        return await _context.TransactionTypes.ToListAsync();
    }

    public async Task<TransactionType> GetById(int id)
    {
        var transactionType=await _context.TransactionTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (transactionType==null)
            return null;
        return transactionType;
    }

    public async Task<TransactionType> Create(TransactionType entity)
    {
        await _context.TransactionTypes.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TransactionType> Update(int id, TransactionType entity)
    {
        var existTransactionType = await _context.TransactionTypes.FindAsync(id);
        if (existTransactionType==null)
            return null;
        

        existTransactionType.Code = entity.Code;
        existTransactionType.Description = entity.Description;
        await _context.SaveChangesAsync();
        return existTransactionType;
    }

    public async Task<TransactionType> Delete(int id)
    {
        var existTransactionType = await _context.TransactionTypes.FindAsync(id);
        if (existTransactionType==null)
            return null;
        
        _context.TransactionTypes.Remove(existTransactionType);
        await _context.SaveChangesAsync();
        return existTransactionType;

    }

    public async Task<bool> IsTransactionTypeExist(int id)
    {
        return await _context.TransactionTypes.AnyAsync(x=>x.Id==id);
    }
}