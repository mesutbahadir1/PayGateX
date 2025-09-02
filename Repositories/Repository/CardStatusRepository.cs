using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class CardStatusRepository:ICardStatusRepository
{
    private readonly ApplicationDBContext _context;

    public CardStatusRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    
    public async Task<List<CardStatus>> GetAll()
    {
        return await _context.CardStatuses.ToListAsync();
    }

    public async Task<CardStatus> GetById(int id)
    {
        var cardStatus=await _context.CardStatuses.FirstOrDefaultAsync(x => x.Id == id);
        if (cardStatus==null)
            return null;
        return cardStatus;
    }

    public async Task<CardStatus> Create(CardStatus entity)
    {
        await _context.CardStatuses.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<CardStatus> Update(int id, CardStatus entity)
    {
        var existCardStatus = await _context.CardStatuses.FindAsync(id);
        if (existCardStatus==null)
            return null;
        

        existCardStatus.Code = entity.Code;
        existCardStatus.Description = entity.Description;
        await _context.SaveChangesAsync();
        return existCardStatus;
    }

    public async Task<CardStatus> Delete(int id)
    {
        var existCardStatus = await _context.CardStatuses.FindAsync(id);
        if (existCardStatus==null)
            return null;

        _context.CardStatuses.Remove(existCardStatus);
        await _context.SaveChangesAsync();
        return existCardStatus;
    }

    public async Task<bool> IsCardStatusExist(int id)
    {
        return await _context.CardStatuses.AnyAsync(x=>x.Id==id);
    }
}