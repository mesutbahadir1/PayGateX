using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.Card;
using PayGateX.Entities;
using PayGateX.Extensions;
using PayGateX.Interfaces;
using PayGateX.Mappers;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CardController:ControllerBase
{
    private readonly ICardRepository _cardRepository;
    private readonly UserManager<AppUser> _userManager; 
    private readonly ICustomerRepository _customerRepository;
    private readonly ICardTypeRepository _cardTypeRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly ICardStatusRepository _cardStatusRepository;
    public CardController(UserManager<AppUser> userManager,ICardRepository cardRepository, ICustomerRepository customerRepository,
        ICardTypeRepository cardTypeRepository, IProductTypeRepository productTypeRepository, ICardStatusRepository cardStatusRepository)
    {
        _userManager = userManager;
        _cardRepository = cardRepository;
        _customerRepository = customerRepository;
        _cardTypeRepository = cardTypeRepository;
        _productTypeRepository = productTypeRepository;
        _cardStatusRepository = cardStatusRepository;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllCards()
    {
        var allCards = await _cardRepository.GetAllCard();
        return Ok(allCards.Select(x=>x.ToCardDto()));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardById([FromRoute] int id)
    {
        var card = await _cardRepository.GetCardById(id);
        if (card==null)
        {
            return NotFound("Card doesn't exist");
        }

        return Ok(card.ToCardDto());
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("{customerId}/{cardTypeId}/{cardStatusId}/{productTypeId}")]
    public async Task<IActionResult> CreateCard([FromRoute] int customerId,[FromRoute] int cardTypeId,[FromRoute] int cardStatusId,
        [FromRoute] int productTypeId,[FromBody] CreateCardDto createCardDto)
    {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);

        var isCustomerExist = await _customerRepository.IsCustomerExist(customerId);
        if (!isCustomerExist)
        {
            return BadRequest("Customer not found");
        }
        
        
        var isCardTypeExist = await _cardTypeRepository.IsCardTypeExist(cardTypeId);
        if (!isCardTypeExist)
        {
            return BadRequest("Card type not found");
        }
        
        var isProductTypeExist = await _productTypeRepository.IsProductTypeExist(productTypeId);
        if (!isProductTypeExist)
        {
            return BadRequest("Product type not found");
        }
        
        var isCardStatusExist = await _cardStatusRepository.IsCardStatusExist(cardStatusId);
        if (!isCardStatusExist)
        {
            return BadRequest("Card status not found");
        }
        
        var cardModel = createCardDto.ToCardFromCreateDto(customerId,appUser.Id,cardTypeId,cardStatusId,productTypeId);

        await _cardRepository.CreateCard(cardModel);

        return Ok(cardModel.ToCardDto());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCard([FromRoute] int id,[FromBody] UpdateCardDto updateCardDto)
    {
        var cardModel = await _cardRepository.UpdateCard(id, updateCardDto.ToCardFromUpdateDto());
        if (cardModel==null)
        {
            return NotFound("Card not found");
        }

        return Ok(cardModel.ToCardDto());
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCard([FromRoute] int id)
    {
        var cardModel = await _cardRepository.DeleteCard(id);
        if (cardModel==null)
        {
            return NotFound("Card not found");
        }

        return Ok(cardModel.ToCardDto());
    }
    
    
    
    
    
}