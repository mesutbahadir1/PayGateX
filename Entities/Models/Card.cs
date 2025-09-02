namespace PayGateX.Entities;

public class Card
{
    public int Id { get; set; }
    public string CardHolder { get; set; }
    public string CardNumber { get; set; }
    public string ExpiryMonth { get; set; }
    public string ExpiryYear { get; set; }
    public string CVV { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastTransactionAt { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
	
    public int CardTypeId { get; set; }
    public CardType CardType { get; set; }

    public int CardStatusId { get; set; }
    public CardStatus CardStatus { get; set; }

    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }


    // Navigation
    public CardLimit CardLimit { get; set; }
    public List<Transaction> Transactions { get; set; }
}