using PayGateX.Dtos.Transaction;
using PayGateX.Entities;

namespace PayGateX.Mappers;

public static class TransactionMappers
{
    
    public static TransactionDto ToTransactionDto(this Transaction transaction)
    {
        return new TransactionDto()
        {
           Id = transaction.Id,
           Amount = transaction.Amount,
           Description = transaction.Description,
           TransactionDate = transaction.TransactionDate,
           CardId = transaction.CardId,
           AppUserId = transaction.AppUserId,
           TransactionTypeId = transaction.TransactionTypeId,
           PaymentMethodTypeId = transaction.PaymentMethodTypeId,
           CurrencyId = transaction.CurrencyId
        };
    }
    
    public static Transaction ToTransactionFromCreateDto(this CreateTransactionDto createTransactionDto,int cardId,string appUserId
        ,int transactionTypeId, int paymentMethodTypeId,int currencyId)
    {
        return new Transaction()
        {
            Amount = createTransactionDto.Amount,
            Description = createTransactionDto.Description,
            CardId = cardId,
            AppUserId = appUserId,
            TransactionTypeId = transactionTypeId,
            PaymentMethodTypeId = paymentMethodTypeId,
            CurrencyId =currencyId
        };
    }
    
    public static Transaction ToTransactionFromUpdateDto(this UpdateTransactionDto updateTransactionDto)
    {
        return new Transaction()
        {
            Amount = updateTransactionDto.Amount,
            Description = updateTransactionDto.Description,
        };
    }
}