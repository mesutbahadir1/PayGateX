using PayGateX.Dtos.Card;
using PayGateX.Entities;

namespace PayGateX.Mappers;

public static class CardMappers
{
    
    public static CardDto ToCardDto(this Card card)
    {
        return new CardDto()
        {
            Id = card.Id,
            CardHolder=card.CardHolder,
            CardNumber=card.CardNumber,
            ExpiryMonth=card.ExpiryMonth,
            ExpiryYear=card.ExpiryYear,
            CVV=card.CVV,
            CreatedAt=card.CreatedAt,
            LastTransactionAt=card.LastTransactionAt,
            CustomerId=card.CustomerId,
            AppUserId=card.AppUserId,
            CardTypeId=card.CardTypeId,
            CardStatusId=card.CardStatusId,
            ProductTypeId=card.ProductTypeId
        };
    }
    
    public static Card ToCardFromCreateDto(this CreateCardDto cardDto,int customerId,string appUserId
        ,int cardTypeId, int cardStatusId,int productTypeId)
    {
        return new Card()
        {
            CardHolder=cardDto.CardHolder,
            CardNumber=cardDto.CardNumber,
            ExpiryMonth=cardDto.ExpiryMonth,
            ExpiryYear=cardDto.ExpiryYear,
            CVV=cardDto.CVV,
            CustomerId=customerId,
            AppUserId=appUserId,
            CardTypeId=cardTypeId,
            CardStatusId=cardStatusId,
            ProductTypeId=productTypeId
        };
    }
    
    public static Card ToCardFromUpdateDto(this UpdateCardDto updateCardDto)
    {
        return new Card()
        {
            CardHolder=updateCardDto.CardHolder,
            CardNumber=updateCardDto.CardNumber,
            ExpiryMonth=updateCardDto.ExpiryMonth,
            ExpiryYear=updateCardDto.ExpiryYear,
            CVV=updateCardDto.CVV
        };
    }
    
}