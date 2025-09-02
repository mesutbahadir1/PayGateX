using PayGateX.Dtos.PaymentMethodType;
using PayGateX.Entities;

namespace PayGateX.Mappers;

public static class PaymentMethodTypeMappers
{
    public static PaymentMethodTypeDto ToPaymentMethodTypeDto(this PaymentMethodType paymentMethodType)
    {
        return new PaymentMethodTypeDto()
        {
            Id = paymentMethodType.Id,
            Code = paymentMethodType.Code,
            Description = paymentMethodType.Description
        };
    }
    
    
    public static PaymentMethodType ToPaymentMethodTypeFromCreateDto(this CreatePaymentMethodTypeDto createPaymentMethodTypeDto)
    {
        return new PaymentMethodType()
        {
            Code = createPaymentMethodTypeDto.Code,
            Description = createPaymentMethodTypeDto.Description
        };
    }
    
    public static PaymentMethodType ToPaymentMethodTypeFromUpdateDto(this UpdatePaymentMethodTypeDto updatePaymentMethodTypeDto)
    {
        return new PaymentMethodType()
        {
            Description = updatePaymentMethodTypeDto.Description,
            Code = updatePaymentMethodTypeDto.Code
        };
    }
}