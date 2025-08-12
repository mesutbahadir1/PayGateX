using PayGateX.Entities;

namespace PayGateX.Interfaces;

public interface ICardLimitRepository
{
    Task<List<CardLimit>> GetAllCardLimits();
    
    Task<CardLimit> GetCardLimitById(int id);
    
    Task<CardLimit> CreateCardLimit(CardLimit cardLimit);
    
    Task<CardLimit> UpdateCardLimit(int id, CardLimit cardLimit);
    
    Task<CardLimit> DeleteCardLimit(int id);
    
    Task<CardLimit> GetCardLimitByCardId(int cardId);
}