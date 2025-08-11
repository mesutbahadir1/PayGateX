using Microsoft.AspNetCore.Identity;

namespace PayGateX.Entities;

public class AppUser:IdentityUser
{
    public string FullName { get; set; }
    
    public string Role { get; set; }

    // Navigation Properties
    public List<Customer> CreatedCustomers { get; set; }      // Kendi oluşturduğu müşteriler
    public List<Card> CreatedCards { get; set; }              // Kendi oluşturduğu kartlar
    public List<Transaction> CreatedTransactions { get; set; } // Oluşturduğu işlemler
}