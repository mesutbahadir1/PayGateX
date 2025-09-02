using System.ComponentModel.DataAnnotations;

namespace PayGateX.Dtos.Card;

public class UpdateCardDto
{
    [Required] 
    public string CardHolder { get; set; }
    [Required] 
    public string CardNumber { get; set; }
    [Required] 
    public string ExpiryMonth { get; set; }
    [Required] 
    public string ExpiryYear { get; set; }
    [Required] 
    public string CVV { get; set; }
}