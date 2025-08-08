using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayGateX.Dtos.Card;
using PayGateX.Dtos.Transaction;
using PayGateX.Entities;
using PayGateX.Extensions;
using PayGateX.Interfaces;
using PayGateX.Mappers;

namespace PayGateX.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TransactionController:ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly ICardRepository _cardRepository;
    private readonly ITransactionTypeRepository _transactionTypeRepository;
    private readonly IPaymentMethodTypeRepository _paymentMethodTypeRepository;
    private readonly ICurrencyRepository _currencyRepository;
    public TransactionController(ITransactionRepository transactionRepository, UserManager<AppUser> userManager, ICardRepository cardRepository, ITransactionTypeRepository transactionTypeRepository, IPaymentMethodTypeRepository paymentMethodTypeRepository, ICurrencyRepository currencyRepository)
    {
        _transactionRepository = transactionRepository;
        _userManager = userManager;
        _cardRepository = cardRepository;
        _transactionTypeRepository = transactionTypeRepository;
        _paymentMethodTypeRepository = paymentMethodTypeRepository;
        _currencyRepository = currencyRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTransactions()
    {
        var allTransactions = await _transactionRepository.GetAllTransaction();
        return Ok(allTransactions.Select(x=>x.ToTransactionDto()));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById([FromRoute] int id)
    {
        var transaction = await _transactionRepository.GetTransactionById(id);
        if (transaction==null)
        {
            return NotFound("Transaction doesn't exist");
        }

        return Ok(transaction.ToTransactionDto());
    }
    
    [Authorize]
    [HttpPost("{cardId}/{transactionTypeId}/{paymentMethodTypeId}/{currencyId}")]
    public async Task<IActionResult> CreateTransaction([FromRoute] int cardId,[FromRoute] int transactionTypeId,[FromRoute] int paymentMethodTypeId,
        [FromRoute] int currencyId,[FromBody] CreateTransactionDto createTransactionDto)
    {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);

        var isCardExist = await _cardRepository.IsCardExist(cardId);
        if (!isCardExist)
        {
            return BadRequest("Card not found");
        }
        
        
        var isTransactionTypeExist = await _transactionTypeRepository.IsTransactionTypeExist(transactionTypeId);
        if (!isTransactionTypeExist)
        {
            return BadRequest("Transaction type not found");
        }
        
        var isPaymentMethodTypeExist = await _paymentMethodTypeRepository.IsPaymentMethodTypeExist(paymentMethodTypeId);
        if (!isPaymentMethodTypeExist)
        {
            return BadRequest("Payment method type not found");
        }
        
        var isCurrencyExist = await _currencyRepository.IsCurrencyExist(currencyId);
        if (!isCurrencyExist)
        {
            return BadRequest("Currency not found");
        }
        
        var transactionModel = createTransactionDto.ToTransactionFromCreateDto(cardId,appUser.Id,transactionTypeId,paymentMethodTypeId,currencyId);

        await _transactionRepository.CreateTransaction(transactionModel);

        return Ok(transactionModel.ToTransactionDto());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaction([FromRoute] int id,[FromBody] UpdateTransactionDto updateTransactionDto)
    {
        var transactionModel = await _transactionRepository.UpdateTransaction(id, updateTransactionDto.ToTransactionFromUpdateDto());
        if (transactionModel==null)
        {
            return NotFound("Transaction not found");
        }

        return Ok(transactionModel.ToTransactionDto());
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction([FromRoute] int id)
    {
        var transactionModel = await _transactionRepository.DeleteTransaction(id);
        if (transactionModel==null)
        {
            return NotFound("Transaction not found");
        }

        return Ok(transactionModel.ToTransactionDto());
    }

    
    
}