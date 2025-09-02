using Microsoft.EntityFrameworkCore;
using PayGateX.Data;
using PayGateX.Entities;
using PayGateX.Interfaces;

namespace PayGateX.Repository;

public class CardRepository:ICardRepository
{
    private readonly ApplicationDBContext _context;
    public CardRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Card>> GetAllCard()
    {
       return await _context.Cards.ToListAsync();
    }

    public async Task<Card> GetCardById(int id)
    {
        var card = await _context.Cards.FindAsync(id);
        if (card==null)
            return null;
        
        return card;
    }

    public async Task<Card> CreateCard(Card card)
    {
        await _context.Cards.AddAsync(card);
        await _context.SaveChangesAsync();
        return card;
    }

    public async Task<Card> UpdateCard(int id, Card card)
    {
        var existCard = await _context.Cards.FindAsync(id);
        if (existCard==null)
        {
            return null;
        }
        existCard.CardHolder = card.CardHolder;
        existCard.CardNumber = card.CardNumber;
        existCard.ExpiryMonth = card.ExpiryMonth;
        existCard.ExpiryYear = card.ExpiryYear;
        existCard.CVV = card.CVV;
        
        await _context.SaveChangesAsync();
        return existCard;
    }

    public async Task<Card> DeleteCard(int id)
    {
        var existCard = await _context.Cards.FindAsync(id);
        if (existCard==null)
        {
            return null;
        }

        _context.Cards.Remove(existCard);
        await _context.SaveChangesAsync();
        return existCard;
    }

    public async Task<bool> IsCardExist(int id)
    {
        return await _context.Cards.AnyAsync(x=>x.Id==id);
    }
}