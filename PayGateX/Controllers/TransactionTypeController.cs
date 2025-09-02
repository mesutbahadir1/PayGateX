using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.TransactionType;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class TransactionTypeController:ControllerBase
{
    private readonly ITransactionTypeService _transactionTypeService;
    public TransactionTypeController(ITransactionTypeService transactionTypeService)
    {
        _transactionTypeService = transactionTypeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTransactionTypes()
    {
        var transactionTypes = await _transactionTypeService.GetAll();;
        var transactionTypesDto = transactionTypes.Select(x => x.ToTransactionTypeDto());
        return Ok(transactionTypesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionTypeById([FromRoute]int id)
    {
        var transactionType = await _transactionTypeService.GetById(id);
        if (transactionType==null)
            return NotFound("Transaction type not found");
        return Ok(transactionType.ToTransactionTypeDto());
    } 
    
    [HttpPost]
    public async Task<IActionResult> CreateTransactionType([FromBody]CreateTransactionTypeDto transactionTypeDto)
    {
        var transactionType = transactionTypeDto.ToTransactionTypeFromCreateDto();
        var createTransactionType = await _transactionTypeService.Create(transactionType);
        return CreatedAtAction(nameof(GetTransactionTypeById), new { id = createTransactionType.Id }, createTransactionType.ToTransactionTypeDto());
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransactionType([FromRoute]int id,[FromBody] UpdateTransactionTypeDto transactionTypeDto)
    {
        var transactionType = await _transactionTypeService.Update(id, transactionTypeDto.ToTransactionTypeFromUpdateDto());
        if (transactionType == null)
            return NotFound("Transaction type not found");

        return Ok(transactionType.ToTransactionTypeDto());
    } 
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCardStatus([FromRoute] int id)
    {
        var deletedTransactionType = await _transactionTypeService.Delete(id);
        if (deletedTransactionType == null)
            return NotFound("Transaction type not found");
        
        return Ok(deletedTransactionType.ToTransactionTypeDto());
    }
}