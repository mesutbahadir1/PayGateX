using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface ICardRepository
{
    Task<List<Card>> GetAllCard();
    
    Task<Card> GetCardById(int id);
    
    Task<Card> CreateCard(Card card);
    
    Task<Card> UpdateCard(int id, Card card);
    
    Task<Card> DeleteCard(int id);
    
    Task<bool> IsCardExist(int id);
}