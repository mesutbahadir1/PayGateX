using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.CardType;
using PayGateX.Interfaces;
using PayGateX.Mappers;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CardTypeController:ControllerBase
{
    private readonly ICardTypeRepository _cardTypeRepository;
    public CardTypeController(ICardTypeRepository cardTypeRepository)
    {
        _cardTypeRepository = cardTypeRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllCardTypes()
    {
        var cardTypes = await _cardTypeRepository.GetAll();
        var cardTypesDto = cardTypes.Select(x => x.ToCardTypeDto());
        return Ok(cardTypesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardTypesById([FromRoute]int id)
    {
        var cardType = await _cardTypeRepository.GetById(id);
        if (cardType==null)
            return NotFound("Card type not found");
        
        return Ok(cardType.ToCardTypeDto());
    } 
    
    [HttpPost]
    public async Task<IActionResult> CreateCardType([FromBody]CreateCardTypeDto cardTypeDto)
    {
        var cardType = cardTypeDto.ToCardTypeFromCreateDto();
        var createCardType = await _cardTypeRepository.Create(cardType);
        return CreatedAtAction(nameof(GetCardTypesById), new { id = createCardType.Id }, createCardType.ToCardTypeDto());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCardType([FromRoute]int id,[FromBody] UpdateCardTypeDto cardTypeDto)
    {
        var cardType = await _cardTypeRepository.Update(id, cardTypeDto.ToCardTypeFromUpdateDto());
        if (cardType == null)
            return NotFound("Card type not found");

        return Ok(cardType.ToCardTypeDto());
    } 
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCardType([FromRoute] int id)
    {
        var deletedCardType = await _cardTypeRepository.Delete(id);
        if (deletedCardType == null)
        {
            return NotFound("Card Type not found");
        }
        return Ok(deletedCardType.ToCardTypeDto());
    }
    
}