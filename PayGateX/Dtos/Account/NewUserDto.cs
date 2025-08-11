namespace PayGateX.Dtos.Account;

public class NewUserDto
{
    public string? UserName { get; set; } = null;
    public string? FullName { get; set; } = null;
    public string? Role { get; set; } = null;
    public string? Email { get; set; } = null;
    public string? Token { get; set; } = null;
}