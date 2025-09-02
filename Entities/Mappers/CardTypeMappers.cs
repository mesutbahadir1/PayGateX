using PayGateX.Dtos.CardType;
using PayGateX.Entities;

namespace PayGateX.Mappers;

public static class CardTypeMappers
{
    public static CardTypeDto ToCardTypeDto(this CardType cardType)
    {
        return new CardTypeDto()
        {
            Id = cardType.Id,
            Code = cardType.Code,
            Description = cardType.Description
        };
    }
    
    
    public static CardType ToCardTypeFromCreateDto(this CreateCardTypeDto cardTypeDto)
    {
        return new CardType()
        {
            Code = cardTypeDto.Code,
            Description = cardTypeDto.Description
        };
    }
    
    public static CardType ToCardTypeFromUpdateDto(this UpdateCardTypeDto updateCardTypeDto)
    {
        return new CardType()
        {
            Description = updateCardTypeDto.Description,
            Code = updateCardTypeDto.Code
        };
    }
}