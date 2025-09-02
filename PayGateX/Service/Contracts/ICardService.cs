using PayGateX.Dtos.Card;
using PayGateX.Entities;

namespace PayGateX.Service.Contracts;

public interface ICardService
{
    Task<List<Card>> GetAllCards();
    
    Task<Card> GetCardById(int id);
    
    Task<Card> CreateCard(int customerId,int cardTypeId,int cardStatusId,
        int productTypeId,CreateCardDto createCardDto, string userName);
    
    Task<Card> UpdateCard(int id, Card card);
    
    Task<Card> DeleteCard(int id);
    
}