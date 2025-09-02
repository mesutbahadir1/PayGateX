using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.Card;
using PayGateX.Entities;
using PayGateX.Extensions;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CardController:ControllerBase
{
    private readonly ICardService _cardService;
    
    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    //[Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllCards()
    {
        var allCards = await _cardService.GetAllCards();
        return Ok(allCards.Select(x=>x.ToCardDto()));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardById([FromRoute] int id)
    {
        var card = await _cardService.GetCardById(id);
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

        var cardModel = await _cardService.CreateCard(customerId, cardTypeId, cardStatusId, productTypeId,
            createCardDto, userName);
        if (cardModel==null)
        {
            return BadRequest("Card not created. Check the information.");
        }

        return Ok(cardModel.ToCardDto());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCard([FromRoute] int id,[FromBody] UpdateCardDto updateCardDto)
    {
        var cardModel = await _cardService.UpdateCard(id, updateCardDto.ToCardFromUpdateDto());
        if (cardModel==null)
        {
            return NotFound("Card not found");
        }

        return Ok(cardModel.ToCardDto());
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCard([FromRoute] int id)
    {
        var cardModel = await _cardService.DeleteCard(id);
        if (cardModel==null)
        {
            return NotFound("Card not found");
        }

        return Ok(cardModel.ToCardDto());
    }
    
}