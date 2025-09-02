namespace PayGateX.Dtos.Customer;

public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string CustomerNumber { get; set; }
    public int CustomerTypeId { get; set; }
    public string AppUserId { get; set; }
}