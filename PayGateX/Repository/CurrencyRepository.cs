using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class CurrencyRepository:ICurrencyRepository
{
    private readonly ApplicationDBContext _context;
    public CurrencyRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Currency>> GetAll()
    {
        return await _context.Currencies.ToListAsync();
    }

    public async Task<Currency> GetById(int id)
    {
        var currency=await _context.Currencies.FirstOrDefaultAsync(x => x.Id == id);
        if (currency==null)
            return null;
        return currency;
    }

    public async Task<Currency> Create(Currency entity)
    {
        await _context.Currencies.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Currency> Update(int id, Currency entity)
    {
        var existCurrency = await _context.Currencies.FindAsync(id);
        if (existCurrency==null)
            return null;
        

        existCurrency.Code = entity.Code;
        existCurrency.Description = entity.Description;
        await _context.SaveChangesAsync();
        return existCurrency;
    }

    public async Task<Currency> Delete(int id)
    {
        var existCurrency = await _context.Currencies.FindAsync(id);
        if (existCurrency==null)
            return null;
        
        _context.Currencies.Remove(existCurrency);
        await _context.SaveChangesAsync();
        return existCurrency;
    }

    public async Task<bool> IsCurrencyExist(int id)
    {
        return await _context.Currencies.AnyAsync(x=>x.Id==id);
    }
}