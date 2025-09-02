using PayGateX.Dtos.Currency;
using PayGateX.Entities;

namespace PayGateX.Mappers;

public static class CurrencyMappers
{
    public static CurrencyDto ToCurrencyDto(this Currency currency)
    {
        return new CurrencyDto()
        {
            Id = currency.Id,
            Code = currency.Code,
            Description = currency.Description
        };
    }
    
    
    public static Currency ToCurrencyFromCreateDto(this CreateCurrencyDto currencyDto)
    {
        return new Currency()
        {
            Code = currencyDto.Code,
            Description = currencyDto.Description
        };
    }
    
    public static Currency ToCurrencyFromUpdateDto(this UpdateCurrencyDto updateCurrencyDto)
    {
        return new Currency()
        {
            Description = updateCurrencyDto.Description,
            Code = updateCurrencyDto.Code
        };
    }
}