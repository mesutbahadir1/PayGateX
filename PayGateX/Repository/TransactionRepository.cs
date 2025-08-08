using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class TransactionRepository:ITransactionRepository
{
    private readonly ApplicationDBContext _context;
    public TransactionRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Transaction>> GetAllTransaction()
    {
        return await _context.Transactions.ToListAsync();
    }

    public async Task<Transaction> GetTransactionById(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction==null)
            return null;
        
        return transaction;
    }

    public async Task<Transaction> CreateTransaction(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction> UpdateTransaction(int id, Transaction transaction)
    {
        var existTransaction = await _context.Transactions.FindAsync(id);
        if (existTransaction==null)
            return null;

        existTransaction.Amount = transaction.Amount;
        existTransaction.Description = transaction.Description;
        await _context.SaveChangesAsync();
        return existTransaction;
    }

    public async Task<Transaction> DeleteTransaction(int id)
    {
        var existTransaction = await _context.Transactions.FindAsync(id);
        if (existTransaction==null)
            return null;

        _context.Transactions.Remove(existTransaction);
        await _context.SaveChangesAsync();
        return existTransaction;
    }
}