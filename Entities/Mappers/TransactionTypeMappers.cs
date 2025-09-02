using PayGateX.Dtos.TransactionType;
using PayGateX.Entities;

namespace PayGateX.Mappers;

public static class TransactionTypeMappers
{
    public static TransactionTypeDto ToTransactionTypeDto(this TransactionType transactionType)
    {
        return new TransactionTypeDto()
        {
            Id = transactionType.Id,
            Code = transactionType.Code,
            Description = transactionType.Description
        };
    }
    
    
    public static TransactionType ToTransactionTypeFromCreateDto(this CreateTransactionTypeDto createTransactionTypeDto)
    {
        return new TransactionType()
        {
            Code = createTransactionTypeDto.Code,
            Description = createTransactionTypeDto.Description
        };
    }
    
    public static TransactionType ToTransactionTypeFromUpdateDto(this UpdateTransactionTypeDto updateTransactionTypeDto)
    {
        return new TransactionType()
        {
            Description = updateTransactionTypeDto.Description,
            Code = updateTransactionTypeDto.Code
        };
    }
}