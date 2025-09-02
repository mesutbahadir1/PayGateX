using PayGateX.Dtos.CardStatus;
using PayGateX.Entities;

namespace PayGateX.Mappers;

public static class CardStatusMappers
{
    public static CardStatusDto ToCardStatusDto(this CardStatus cardStatus)
    {
        return new CardStatusDto()
        {
            Id = cardStatus.Id,
            Code = cardStatus.Code,
            Description = cardStatus.Description
        };
    }
    
    
    public static CardStatus ToCardStatusFromCreateDto(this CreateCardStatusDto createCardStatusDto)
    {
        return new CardStatus()
        {
            Code = createCardStatusDto.Code,
            Description = createCardStatusDto.Description
        };
    }
    
    public static CardStatus ToCardStatusFromUpdateDto(this UpdateCardStatusDto updateCardStatusDto)
    {
        return new CardStatus()
        {
            Description = updateCardStatusDto.Description,
            Code = updateCardStatusDto.Code
        };
    }
}