using Microsoft.AspNetCore.Identity;
using PayGateX.Dtos.Card;
using PayGateX.Entities;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Service;

public class CardService:ICardService
{
    private readonly ICardRepository _cardRepository;
    private readonly UserManager<AppUser> _userManager; 
    private readonly ICustomerRepository _customerRepository;
    private readonly ICardTypeRepository _cardTypeRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly ICardStatusRepository _cardStatusRepository;
    public CardService(ICardRepository cardRepository, UserManager<AppUser> userManager, ICustomerRepository customerRepository, ICardTypeRepository cardTypeRepository, IProductTypeRepository productTypeRepository, ICardStatusRepository cardStatusRepository)
    {
        _cardRepository = cardRepository;
        _userManager = userManager;
        _customerRepository = customerRepository;
        _cardTypeRepository = cardTypeRepository;
        _productTypeRepository = productTypeRepository;
        _cardStatusRepository = cardStatusRepository;
    }
    
    
    public async Task<List<Card>> GetAllCards()
    {
        var allCards = await _cardRepository.GetAllCard();
        return allCards;
    }

    public async Task<Card> GetCardById(int id)
    {
        var card = await _cardRepository.GetCardById(id);
        return card;
    }

    public async Task<Card> CreateCard(int customerId,int cardTypeId,int cardStatusId,
        int productTypeId,CreateCardDto createCardDto, string userName)
    {
       
        var appUser = await _userManager.FindByNameAsync(userName);

        var isCustomerExist = await _customerRepository.IsCustomerExist(customerId);
        if (!isCustomerExist)
        {
            return null;
        }
        
        
        var isCardTypeExist = await _cardTypeRepository.IsCardTypeExist(cardTypeId);
        if (!isCardTypeExist)
        {
            return null;
        }
        
        var isProductTypeExist = await _productTypeRepository.IsProductTypeExist(productTypeId);
        if (!isProductTypeExist)
        {
            return null;
        }
        
        var isCardStatusExist = await _cardStatusRepository.IsCardStatusExist(cardStatusId);
        if (!isCardStatusExist)
        {
            return null;
        }
        
        var cardModel = createCardDto.ToCardFromCreateDto(customerId,appUser.Id,cardTypeId,cardStatusId,productTypeId);

        await _cardRepository.CreateCard(cardModel);

        return cardModel;
    }

    public async Task<Card> UpdateCard(int id, Card card)
    {
        var cardModel = await _cardRepository.UpdateCard(id, card);
        return cardModel;
    }

    public async Task<Card> DeleteCard(int id)
    {
        var cardModel = await _cardRepository.DeleteCard(id);
        return cardModel;
    }
}