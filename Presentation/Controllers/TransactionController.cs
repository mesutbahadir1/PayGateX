using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.Card;
using PayGateX.Dtos.Transaction;
using PayGateX.Entities;
using PayGateX.Extensions;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class TransactionController:ControllerBase
{
    private readonly ITransactionService _transactionService;
    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTransactions()
    {
        var allTransactions = await _transactionService.GetAllTransaction();
        return Ok(allTransactions.Select(x=>x.ToTransactionDto()));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById([FromRoute] int id)
    {
        var transaction = await _transactionService.GetTransactionById(id);
        if (transaction==null)
            return NotFound("Transaction doesn't exist");

        return Ok(transaction.ToTransactionDto());
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("{cardId}/{transactionTypeId}/{paymentMethodTypeId}/{currencyId}")]
    public async Task<IActionResult> CreateTransaction([FromRoute] int cardId,[FromRoute] int transactionTypeId,[FromRoute] int paymentMethodTypeId,
        [FromRoute] int currencyId,[FromBody] CreateTransactionDto createTransactionDto)
    {
        var userName = User.GetUserName();
        var transactionModel = await _transactionService.CreateTransaction(cardId, transactionTypeId,
            paymentMethodTypeId, currencyId, createTransactionDto, userName);
        if (transactionModel == null)
            return BadRequest("Kart limiti yetersiz.");

        return Ok(transactionModel.ToTransactionDto());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaction([FromRoute] int id,[FromBody] UpdateTransactionDto updateTransactionDto)
    {
        var transactionModel = await _transactionService.UpdateTransaction(id, updateTransactionDto.ToTransactionFromUpdateDto());
        if (transactionModel==null)
            return NotFound("Transaction not found");

        return Ok(transactionModel.ToTransactionDto());
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction([FromRoute] int id)
    {
        var transactionModel = await _transactionService.DeleteTransaction(id);
        if (transactionModel==null)
            return NotFound("Transaction not found");

        return Ok(transactionModel.ToTransactionDto());
    }

    
    
}