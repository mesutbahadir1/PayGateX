using System.ComponentModel.DataAnnotations;

namespace PayGateX.Dtos.Transaction;

public class CreateTransactionDto
{
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public string Description { get; set; }
}