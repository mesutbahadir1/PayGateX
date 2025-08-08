using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class CardTypeRepository:ICardTypeRepository
{
    private readonly ApplicationDBContext _context;
    public CardTypeRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<CardType>> GetAll()
    {
        return await _context.CardTypes.ToListAsync();
    }

    public async Task<CardType> GetById(int id)
    {
        var cardType=await _context.CardTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (cardType==null)
        {
            return null;
        }

        return cardType;
    }

    public async Task<CardType> Create(CardType cardType)
    {
        await _context.CardTypes.AddAsync(cardType);
        await _context.SaveChangesAsync();
        return cardType;
    }

    public async Task<CardType> Update(int id, CardType cardType)
    {
        var cardTypeModel = await _context.CardTypes.FindAsync(id);
        if (cardTypeModel==null)
        {
            return null;
        }

        cardTypeModel.Code = cardType.Code;
        cardTypeModel.Description = cardType.Description;
        await _context.SaveChangesAsync();
        return cardTypeModel;
    }

    public async Task<CardType> Delete(int id)
    {
        var cardTypeModel = await _context.CardTypes.FindAsync(id);
        if (cardTypeModel==null)
        {
            return null;
        }

        _context.CardTypes.Remove(cardTypeModel);
        await _context.SaveChangesAsync();
        return cardTypeModel;
    }

    public async Task<bool> IsCardTypeExist(int id)
    {
        return await _context.CardTypes.AnyAsync(x=>x.Id==id);
    }
}