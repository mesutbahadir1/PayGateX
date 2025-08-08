using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class CardLimitRepository:ICardLimitRepository
{
    private readonly ApplicationDBContext _context;
    public CardLimitRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<CardLimit>> GetAllCardLimits()
    {
        return await _context.CardLimits.ToListAsync();
    }

    public async Task<CardLimit> GetCardLimitById(int id)
    {
        var existCardLimit = await _context.CardLimits.FindAsync(id);
        if (existCardLimit == null)
            return null;

        return existCardLimit;
    }

    public async Task<CardLimit> CreateCardLimit(CardLimit cardLimit)
    {
        await _context.CardLimits.AddAsync(cardLimit);
        await _context.SaveChangesAsync();
        return cardLimit;
    }

    public async Task<CardLimit> UpdateCardLimit(int id, CardLimit cardLimit)
    {
        var existCardLimit = await _context.CardLimits.FindAsync(id);
        if (existCardLimit == null)
            return null;
        
        existCardLimit.TotalLimit = cardLimit.TotalLimit;
        existCardLimit.UsedLimit = cardLimit.UsedLimit;
        await _context.SaveChangesAsync();
        return existCardLimit;
    }

    public async Task<CardLimit> DeleteCardLimit(int id)
    {
        var existCardLimit = await _context.CardLimits.FindAsync(id);
        if (existCardLimit == null)
            return null;

        _context.CardLimits.Remove(existCardLimit);
        await _context.SaveChangesAsync();
        return existCardLimit;
    }
}