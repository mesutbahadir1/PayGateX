using Microsoft.AspNetCore.Identity;
using PayGateX.Dtos.CardLimit;
using PayGateX.Entities;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Service;

public class CardLimitService:ICardLimitService
{
    private readonly ICardLimitRepository _cardLimitRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICardRepository _cardRepository;
    public CardLimitService(ICardLimitRepository cardLimitRepository, UserManager<AppUser> userManager, ICurrencyRepository currencyRepository, ICardRepository cardRepository)
    {
        _cardLimitRepository = cardLimitRepository;
        _userManager = userManager;
        _currencyRepository = currencyRepository;
        _cardRepository = cardRepository;
    }
    
    public async Task<List<CardLimit>> GetAllCardLimits()
    {
        var allCardLimits = await _cardLimitRepository.GetAllCardLimits();
        return allCardLimits;
    }

    public async Task<CardLimit> GetCardLimitById(int id)
    {
        var cardLimit = await _cardLimitRepository.GetCardLimitById(id);
        return cardLimit;
    }

    public async Task<CardLimit> CreateCardLimit(int currencyId, int cardId, CreateCardLimitDto createCardLimitDto)
    {
        var isCurrencyExist = await _currencyRepository.IsCurrencyExist(currencyId);
        if (!isCurrencyExist)
            return null;
        
        
        var isCardTExist = await _cardRepository.IsCardExist(cardId);
        if (!isCardTExist)
            return null;
        
        var cardLimitModel = createCardLimitDto.ToCardLimitFromCreateDto(currencyId,cardId);

        await _cardLimitRepository.CreateCardLimit(cardLimitModel);

        return cardLimitModel;
    }

    public async Task<CardLimit> UpdateCardLimit(int id, CardLimit cardLimit)
    {
        var cardLimitModel = await _cardLimitRepository.UpdateCardLimit(id, cardLimit);
        return cardLimitModel;
    }

    public async Task<CardLimit> DeleteCardLimit(int id)
    {
        var cardLimitModel = await _cardLimitRepository.DeleteCardLimit(id);
        return cardLimitModel;

    }
    
}