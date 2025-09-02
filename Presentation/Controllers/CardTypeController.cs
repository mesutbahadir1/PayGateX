using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.CardType;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CardTypeController:ControllerBase
{
    private readonly ICardTypeService _cardTypeService;
    public CardTypeController(ICardTypeService cardTypeService)
    {
        _cardTypeService = cardTypeService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllCardTypes()
    {
        var cardTypes = await _cardTypeService.GetAll();
        var cardTypesDto = cardTypes.Select(x => x.ToCardTypeDto());
        return Ok(cardTypesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardTypesById([FromRoute]int id)
    {
        var cardType = await _cardTypeService.GetById(id);
        if (cardType==null)
            return NotFound("Card type not found");
        
        return Ok(cardType.ToCardTypeDto());
    } 
    
    [HttpPost]
    public async Task<IActionResult> CreateCardType([FromBody]CreateCardTypeDto cardTypeDto)
    {
        var cardType = cardTypeDto.ToCardTypeFromCreateDto();
        var createCardType = await _cardTypeService.Create(cardType);
        return CreatedAtAction(nameof(GetCardTypesById), new { id = createCardType.Id }, createCardType.ToCardTypeDto());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCardType([FromRoute]int id,[FromBody] UpdateCardTypeDto cardTypeDto)
    {
        var cardType = await _cardTypeService.Update(id, cardTypeDto.ToCardTypeFromUpdateDto());
        if (cardType == null)
            return NotFound("Card type not found");

        return Ok(cardType.ToCardTypeDto());
    } 
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCardType([FromRoute] int id)
    {
        var deletedCardType = await _cardTypeService.Delete(id);
        if (deletedCardType == null)
        {
            return NotFound("Card Type not found");
        }
        return Ok(deletedCardType.ToCardTypeDto());
    }
    
}