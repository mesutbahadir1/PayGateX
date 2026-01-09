using Microsoft.AspNetCore.Identity;
using PayGateX.Dtos.Transaction;
using PayGateX.Entities;
using PayGateX.Interfaces;
using PayGateX.Mappers;
using PayGateX.Service.Contracts;

namespace PayGateX.Service;

public class TransactionService:ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly ICardRepository _cardRepository;
    private readonly ITransactionTypeRepository _transactionTypeRepository;
    private readonly IPaymentMethodTypeRepository _paymentMethodTypeRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ILoggerService _loggerService;
    
    public TransactionService(ITransactionRepository transactionRepository, UserManager<AppUser> userManager, ICardRepository cardRepository, ITransactionTypeRepository transactionTypeRepository, IPaymentMethodTypeRepository paymentMethodTypeRepository, ICurrencyRepository currencyRepository, ILoggerService loggerService)
    {
        _transactionRepository = transactionRepository;
        _userManager = userManager;
        _cardRepository = cardRepository;
        _transactionTypeRepository = transactionTypeRepository;
        _paymentMethodTypeRepository = paymentMethodTypeRepository;
        _currencyRepository = currencyRepository;
        _loggerService = loggerService;
    }
    
    public async Task<List<Transaction>> GetAllTransaction()
    {
        var allTransactions = await _transactionRepository.GetAllTransaction();
        return allTransactions;
    }

    public async Task<Transaction> GetTransactionById(int id)
    {
        var transaction = await _transactionRepository.GetTransactionById(id);
        return transaction;
    }

    public async Task<Transaction> CreateTransaction(int cardId, int transactionTypeId, int paymentMethodTypeId,
        int currencyId,CreateTransactionDto createTransactionDto,string userName)
    {
        var appUser = await _userManager.FindByNameAsync(userName);

        var isCardExist = await _cardRepository.IsCardExist(cardId);
        if (!isCardExist)
        {
            _loggerService.LogInfo($"The card with {cardId} didn't found.");
            return null;
        }
        
        var isTransactionTypeExist = await _transactionTypeRepository.IsTransactionTypeExist(transactionTypeId);
        if (!isTransactionTypeExist)
            return null;
        
        var isPaymentMethodTypeExist = await _paymentMethodTypeRepository.IsPaymentMethodTypeExist(paymentMethodTypeId);
        if (!isPaymentMethodTypeExist)
            return null;
        
        var isCurrencyExist = await _currencyRepository.IsCurrencyExist(currencyId);
        if (!isCurrencyExist)
            return null;
        
        var transactionModel = createTransactionDto.ToTransactionFromCreateDto(cardId,appUser.Id,transactionTypeId,paymentMethodTypeId,currencyId);

        var model = await _transactionRepository.CreateTransaction(transactionModel);

        if (model == null)
            return null;
        return model;
    }

    public async Task<Transaction> UpdateTransaction(int id, Transaction transaction)
    {
        var transactionModel = await _transactionRepository.UpdateTransaction(id, transaction);
        return transactionModel;
    }

    public async Task<Transaction> DeleteTransaction(int id)
    {
        var transactionModel = await _transactionRepository.DeleteTransaction(id);
        return transactionModel;
    }
}