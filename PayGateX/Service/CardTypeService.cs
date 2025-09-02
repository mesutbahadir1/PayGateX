using PayGateX.Entities;
using PayGateX.Interfaces;
using PayGateX.Service.Contracts;

namespace PayGateX.Service;

public class CardTypeService:ICardTypeService
{
    private readonly ICardTypeRepository _cardTypeRepository;
    public CardTypeService(ICardTypeRepository cardTypeRepository)
    {
        _cardTypeRepository = cardTypeRepository;
    }
    public async Task<List<CardType>> GetAll()
    {
        var cardTypes = await _cardTypeRepository.GetAll();
        return cardTypes;
    }

    public async Task<CardType> GetById(int id)
    {
        var cardType = await _cardTypeRepository.GetById(id);
        return cardType;
    }

    public async Task<CardType> Create(CardType entity)
    {
        var createCardType = await _cardTypeRepository.Create(entity);
        return createCardType;
    }

    public async Task<CardType> Update(int id, CardType entity)
    {
        var cardType = await _cardTypeRepository.Update(id, entity);
        return cardType;
    }

    public async Task<CardType> Delete(int id)
    {
        var deletedCardType = await _cardTypeRepository.Delete(id);
        return deletedCardType;
    }
}