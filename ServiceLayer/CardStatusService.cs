using PayGateX.Entities;
using PayGateX.Interfaces;
using PayGateX.Service.Contracts;

namespace PayGateX.Service;

public class CardStatusService:ICardStatusService
{
    private readonly ICardStatusRepository _repository;
    public CardStatusService(ICardStatusRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<CardStatus>> GetAll()
    {
        var cardStatus = await _repository.GetAll();;
        return cardStatus;
    }

    public async Task<CardStatus> GetById(int id)
    {
        var cardStatus = await _repository.GetById(id);
        return cardStatus;
    }

    public async Task<CardStatus> Create(CardStatus entity)
    {
        var createCardStatus = await _repository.Create(entity);
        return createCardStatus;
    }

    public async Task<CardStatus> Update(int id, CardStatus entity)
    {
        var cardStatus = await _repository.Update(id, entity);
        return cardStatus;
    }

    public async Task<CardStatus> Delete(int id)
    {
        var deletedCardStatus = await _repository.Delete(id);
        return deletedCardStatus;
    }
}