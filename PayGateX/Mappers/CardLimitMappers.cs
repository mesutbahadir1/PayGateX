using PayGateX.Dtos.CardLimit;
using PayGateX.Entities;

namespace PayGateX.Mappers;

public static class CardLimitMappers
{
    public static CardLimitDto ToCardLimitDto(this CardLimit cardLimit)
    {
        return new CardLimitDto()
        {
           Id = cardLimit.Id,
           TotalLimit = cardLimit.TotalLimit,
           UsedLimit = cardLimit.UsedLimit,
           StartDate = cardLimit.StartDate,
           EndDate = cardLimit.EndDate,
           CurrencyId = cardLimit.CurrencyId,
           CardId = cardLimit.CardId
        };
    }
    
    public static CardLimit ToCardLimitFromCreateDto(this CreateCardLimitDto cardLimitDto,int currencyId,int cardId)
    {
        return new CardLimit()
        {
            TotalLimit = cardLimitDto.TotalLimit,
            UsedLimit = cardLimitDto.UsedLimit,
            CurrencyId = currencyId,
            CardId = cardId
        };
    }
    
    public static CardLimit ToCardLimitFromUpdateDto(this UpdateCardLimitDto updateCardLimitDto)
    {
        return new CardLimit()
        {
            TotalLimit = updateCardLimitDto.TotalLimit,
            UsedLimit = updateCardLimitDto.UsedLimit,
        };
    }
}