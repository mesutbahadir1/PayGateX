using PayGateX.Dtos.CardLimit;
using PayGateX.Entities;

namespace PayGateX.Service.Contracts;

public interface ICardLimitService
{
    Task<List<CardLimit>> GetAllCardLimits();
    
    Task<CardLimit> GetCardLimitById(int id);
    
    Task<CardLimit> CreateCardLimit(int currencyId, int cardId, CreateCardLimitDto createCardLimitDto);
    
    Task<CardLimit> UpdateCardLimit(int id, CardLimit cardLimit);
    
    Task<CardLimit> DeleteCardLimit(int id);
}