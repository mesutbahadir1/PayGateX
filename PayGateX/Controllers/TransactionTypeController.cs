using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.TransactionType;
using PayGateX.Interfaces;
using PayGateX.Mappers;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TransactionTypeController:ControllerBase
{
    private readonly ITransactionTypeRepository _transactionTypeRepository;
    public TransactionTypeController(ITransactionTypeRepository transactionTypeRepository)
    {
        _transactionTypeRepository = transactionTypeRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTransactionTypes()
    {
        var transactionTypes = await _transactionTypeRepository.GetAll();;
        var transactionTypesDto = transactionTypes.Select(x => x.ToTransactionTypeDto());
        return Ok(transactionTypesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionTypeById([FromRoute]int id)
    {
        var transactionType = await _transactionTypeRepository.GetById(id);
        if (transactionType==null)
            return NotFound("Transaction type not found");
        return Ok(transactionType.ToTransactionTypeDto());
    } 
    
    [HttpPost]
    public async Task<IActionResult> CreateTransactionType([FromBody]CreateTransactionTypeDto transactionTypeDto)
    {
        var transactionType = transactionTypeDto.ToTransactionTypeFromCreateDto();
        var createTransactionType = await _transactionTypeRepository.Create(transactionType);
        return CreatedAtAction(nameof(GetTransactionTypeById), new { id = createTransactionType.Id }, createTransactionType.ToTransactionTypeDto());
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransactionType([FromRoute]int id,[FromBody] UpdateTransactionTypeDto transactionTypeDto)
    {
        var transactionType = await _transactionTypeRepository.Update(id, transactionTypeDto.ToTransactionTypeFromUpdateDto());
        if (transactionType == null)
            return NotFound("Transaction type not found");

        return Ok(transactionType.ToTransactionTypeDto());
    } 
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCardStatus([FromRoute] int id)
    {
        var deletedTransactionType = await _transactionTypeRepository.Delete(id);
        if (deletedTransactionType == null)
            return NotFound("Transaction type not found");
        
        return Ok(deletedTransactionType.ToTransactionTypeDto());
    }
}