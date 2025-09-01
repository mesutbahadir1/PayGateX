using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.Card;
using PayGateX.Dtos.CardLimit;
using PayGateX.Entities;
using PayGateX.Extensions;
using PayGateX.Interfaces;
using PayGateX.Mappers;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CardLimitController:ControllerBase
{
    private readonly ICardLimitRepository _cardLimitRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICardRepository _cardRepository;
    public CardLimitController(ICardLimitRepository cardLimitRepository, UserManager<AppUser> userManager, ICurrencyRepository currencyRepository, ICardRepository cardRepository)
    {
        _cardLimitRepository = cardLimitRepository;
        _userManager = userManager;
        _currencyRepository = currencyRepository;
        _cardRepository = cardRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCardLimits()
    {
        var allCardLimits = await _cardLimitRepository.GetAllCardLimits();
        return Ok(allCardLimits.Select(x=>x.ToCardLimitDto()));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardLimitById([FromRoute] int id)
    {
        var cardLimit = await _cardLimitRepository.GetCardLimitById(id);
        if (cardLimit==null)
            return NotFound("Card limit doesn't exist");

        return Ok(cardLimit.ToCardLimitDto());
    }
    
    [HttpPost("{currencyId}/{cardId}")]
    public async Task<IActionResult> CreateCardLimit([FromRoute] int currencyId,[FromRoute] int cardId, [FromBody] CreateCardLimitDto createCardLimitDto)
    {

        var isCurrencyExist = await _currencyRepository.IsCurrencyExist(currencyId);
        if (!isCurrencyExist)
            return BadRequest("Currency not found");
        
        
        var isCardTExist = await _cardRepository.IsCardExist(cardId);
        if (!isCardTExist)
            return BadRequest("Card type not found");
        
        var cardLimitModel = createCardLimitDto.ToCardLimitFromCreateDto(currencyId,cardId);

        await _cardLimitRepository.CreateCardLimit(cardLimitModel);

        return Ok(cardLimitModel.ToCardLimitDto());
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCardLimit([FromRoute] int id,[FromBody] UpdateCardLimitDto updateCardLimitDto)
    {
        var cardLimitModel = await _cardLimitRepository.UpdateCardLimit(id, updateCardLimitDto.ToCardLimitFromUpdateDto());
        if (cardLimitModel==null)
            return NotFound("Card limit not found");

        return Ok(cardLimitModel.ToCardLimitDto());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCardLimit([FromRoute] int id)
    {
        var cardLimitModel = await _cardLimitRepository.DeleteCardLimit(id);
        if (cardLimitModel==null)
            return NotFound("Card limit not found");

        return Ok(cardLimitModel.ToCardLimitDto());
    }
    
}