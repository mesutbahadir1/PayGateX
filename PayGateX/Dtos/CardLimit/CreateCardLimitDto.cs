using System.ComponentModel.DataAnnotations;

namespace PayGateX.Dtos.CardLimit;

public class CreateCardLimitDto
{
    [Required] 
    public decimal TotalLimit { get; set; }
    [Required] 
    public decimal UsedLimit { get; set; }
}