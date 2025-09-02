using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.CardStatus;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CardStatusController:ControllerBase
{
    private readonly ICardStatusService _cardStatusService;
    public CardStatusController(ICardStatusService cardStatusService)
    {
        _cardStatusService = cardStatusService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCardStatus()
    {
        var cardStatus = await _cardStatusService.GetAll();;
        var cardStatusDto = cardStatus.Select(x => x.ToCardStatusDto());
        return Ok(cardStatusDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardStatusById([FromRoute]int id)
    {
        var cardStatus = await _cardStatusService.GetById(id);
        if (cardStatus==null)
            return NotFound("Card status not found");
        
        return Ok(cardStatus.ToCardStatusDto());
    } 
    
    [HttpPost]
    public async Task<IActionResult> CreateCardStatus([FromBody]CreateCardStatusDto cardStatusDto)
    {
        var cardStatus = cardStatusDto.ToCardStatusFromCreateDto();
        var createCardStatus = await _cardStatusService.Create(cardStatus);
        return CreatedAtAction(nameof(GetCardStatusById), new { id = createCardStatus.Id }, createCardStatus.ToCardStatusDto());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCardStatus([FromRoute]int id,[FromBody] UpdateCardStatusDto cardStatusDto)
    {
        var cardStatus = await _cardStatusService.Update(id, cardStatusDto.ToCardStatusFromUpdateDto());
        if (cardStatus == null)
            return NotFound("Card status not found");

        return Ok(cardStatus.ToCardStatusDto());
    } 
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCardStatus([FromRoute] int id)
    {
        var deletedCardStatus = await _cardStatusService.Delete(id);
        if (deletedCardStatus == null)
            return NotFound("Card status not found");
        
        return Ok(deletedCardStatus.ToCardStatusDto());
    }

    
}