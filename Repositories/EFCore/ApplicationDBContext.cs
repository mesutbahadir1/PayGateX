using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayGateX.Entities;

namespace PayGateX.Data;

public class ApplicationDBContext : IdentityDbContext<AppUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }

    // Yeni DbSet tanımlamaları
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<CardLimit> CardLimits { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<CustomerType> CustomerTypes { get; set; }
    public DbSet<CardType> CardTypes { get; set; }
    public DbSet<CardStatus> CardStatuses { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<PaymentMethodType> PaymentMethodTypes { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // AppUser ilişkileri
        builder.Entity<AppUser>()
            .HasMany(u => u.CreatedCustomers)
            .WithOne(c => c.AppUser)
            .HasForeignKey(c => c.AppUserId);

        builder.Entity<AppUser>()
            .HasMany(u => u.CreatedCards)
            .WithOne(c => c.AppUser)
            .HasForeignKey(c => c.AppUserId);

        builder.Entity<AppUser>()
            .HasMany(u => u.CreatedTransactions)
            .WithOne(t => t.AppUser)
            .HasForeignKey(t => t.AppUserId);
        
        
        // Card ilişkileri
        builder.Entity<Card>()
            .HasMany(c => c.Transactions)
            .WithOne(t => t.Card)
            .HasForeignKey(t => t.CardId);
        
        // One-to-one ilişki (Card - CardLimit)
        builder.Entity<Card>()
            .HasOne(c => c.CardLimit)
            .WithOne(cl => cl.Card)
            .HasForeignKey<CardLimit>(cl => cl.CardId);
        
       
        //CardStatus ilişkileri
        builder.Entity<CardStatus>()
            .HasMany(cs => cs.Cards)
            .WithOne(c => c.CardStatus)
            .HasForeignKey(c => c.CardStatusId);
        
        //CardType ilişkileri 
        builder.Entity<CardType>()
            .HasMany(ct => ct.Cards)
            .WithOne(c => c.CardType)
            .HasForeignKey(c => c.CardTypeId);
        
        //Currency ilişkileri
        builder.Entity<Currency>()
            .HasMany(c => c.Transactions)
            .WithOne(t => t.Currency)
            .HasForeignKey(t => t.CurrencyId);
        
        builder.Entity<Currency>()
            .HasMany(c => c.CardLimits)
            .WithOne(cl => cl.Currency)
            .HasForeignKey(cl => cl.CurrencyId);    
        
        // Customer ilişkileri
        builder.Entity<Customer>()
            .HasMany(c => c.Cards)
            .WithOne(card => card.Customer)
            .HasForeignKey(card => card.CustomerId);
    
        // CustomerType ilişkileri
        builder.Entity<CustomerType>()
            .HasMany(ct => ct.Customers)
            .WithOne(c => c.CustomerType)
            .HasForeignKey(c => c.CustomerTypeId);
        
        //PaymentMethodType ilişkileri
        builder.Entity<PaymentMethodType>().
            HasMany(c=>c.Transactions).
            WithOne(cl=>cl.PaymentMethodType).
            HasForeignKey(payment=>payment.PaymentMethodTypeId);
        
        //ProductType ilişkileri
        builder.Entity<ProductType>()
            .HasMany(pt => pt.Cards)
            .WithOne(c => c.ProductType)
            .HasForeignKey(c => c.ProductTypeId);
        
        //TransactionType ilişkileri
        builder.Entity<TransactionType>()
            .HasMany(tt => tt.Transactions)
            .WithOne(t => t.TransactionType)
            .HasForeignKey(t => t.TransactionTypeId);
        
        
        // Role seed (erişim kontrolü için başlangıç rolleri)
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" },
            new IdentityRole { Name = "CustomerSupport", NormalizedName = "CUSTOMERSUPPORT" },
            new IdentityRole { Name = "User", NormalizedName = "USER" }
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}
