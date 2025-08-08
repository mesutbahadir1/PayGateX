using System.ComponentModel.DataAnnotations;

namespace PayGateX.Dtos.Account;

public class RegisterDto
{
    [Required] 
    public string? UserName { get; set; } = null;
    
    [Required] 
    public string? FullName { get; set; } = null;
    
    [Required] 
    [EmailAddress] 
    public string? Email { get; set; } = null;
    
    [Required] 
    public string? Password { get; set; } = null;
}
