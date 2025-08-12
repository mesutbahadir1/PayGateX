using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class TransactionRepository:ITransactionRepository
{
    private readonly ApplicationDBContext _context;
    private readonly ITcmbCurrencyService _currencyService;
    private readonly ICardLimitRepository _cardLimitRepository;
    public TransactionRepository(ApplicationDBContext context, ITcmbCurrencyService currencyService, ICardLimitRepository cardLimitRepository)
    {
        _context = context;
        _currencyService = currencyService;
        _cardLimitRepository = cardLimitRepository;
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
        var usd = await _currencyService.GetUsdTryRateAsync();
        var euro = await _currencyService.GetEurTryRateAsync();
        var cardLimit = await _cardLimitRepository.GetCardLimitByCardId(transaction.CardId);

        var transactionAmount = transaction.Amount;
        if (transaction.CurrencyId==4)
        {
            transactionAmount *= usd;
        }
        if (transaction.CurrencyId == 5)
        {
            transactionAmount *= euro;
        }
        

        if (transactionAmount<=cardLimit.AvailableLimit)
        {
            cardLimit.UsedLimit += transactionAmount;
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
        else
        {
            return null;
        }
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