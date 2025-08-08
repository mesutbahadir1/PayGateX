using System.ComponentModel.DataAnnotations;

namespace PayGateX.Dtos.Currency;

public class CreateCurrencyDto
{
    [Required]
    [MinLength(2,ErrorMessage = "Code must be min 2 character")]
    [MaxLength(20,ErrorMessage = "Code  must be max 20 character")]
    public string Code { get; set; }
    
    [Required]
    [MinLength(5,ErrorMessage = "Description must be min 5 character")]
    [MaxLength(280,ErrorMessage = "Description must be max 280 character")]
    public string Description { get; set; }
}