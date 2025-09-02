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
using PayGateX.Service.Contracts;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CardLimitController:ControllerBase
{
    private readonly ICardLimitService _cardLimitService;
   
    public CardLimitController(ICardLimitService cardLimitService)
    {
        _cardLimitService = cardLimitService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCardLimits()
    {
        var allCardLimits = await _cardLimitService.GetAllCardLimits();
        return Ok(allCardLimits.Select(x=>x.ToCardLimitDto()));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardLimitById([FromRoute] int id)
    {
        var cardLimit = await _cardLimitService.GetCardLimitById(id);
        if (cardLimit==null)
            return NotFound("Card limit doesn't exist");

        return Ok(cardLimit.ToCardLimitDto());
    }
    
    [HttpPost("{currencyId}/{cardId}")]
    public async Task<IActionResult> CreateCardLimit([FromRoute] int currencyId,[FromRoute] int cardId, [FromBody] CreateCardLimitDto createCardLimitDto)
    {

        var cardLimitModel = await _cardLimitService.CreateCardLimit(currencyId, cardId, createCardLimitDto);

        if (cardLimitModel == null)
            return BadRequest("Card limit not created. Check the information.");
        
        return Ok(cardLimitModel.ToCardLimitDto());
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCardLimit([FromRoute] int id,[FromBody] UpdateCardLimitDto updateCardLimitDto)
    {
        var cardLimitModel = await _cardLimitService.UpdateCardLimit(id, updateCardLimitDto.ToCardLimitFromUpdateDto());
        if (cardLimitModel==null)
            return NotFound("Card limit not found");

        return Ok(cardLimitModel.ToCardLimitDto());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCardLimit([FromRoute] int id)
    {
        var cardLimitModel = await _cardLimitService.DeleteCardLimit(id);
        if (cardLimitModel==null)
            return NotFound("Card limit not found");

        return Ok(cardLimitModel.ToCardLimitDto());
    }
    
}