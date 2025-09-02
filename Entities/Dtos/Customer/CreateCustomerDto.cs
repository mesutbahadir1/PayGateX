using System.ComponentModel.DataAnnotations;

namespace PayGateX.Dtos.Customer;

public class CreateCustomerDto
{
    [Required] 
    public string Name { get; set; }
    [Required] 
    public string Surname { get; set; }
    [Required] 
    [EmailAddress] 
    public string Email { get; set; }
    [Required] 
    public string CustomerNumber { get; set; }
}