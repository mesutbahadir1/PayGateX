using System.ComponentModel.DataAnnotations;

namespace PayGateX.Dtos.CardLimit;

public class UpdateCardLimitDto
{
    [Required] 
    public decimal TotalLimit { get; set; }
    [Required] 
    public decimal UsedLimit { get; set; }
}