namespace PayGateX.Entities;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }= DateTime.UtcNow;

    public int CardId { get; set; }
    public Card Card { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public int TransactionTypeId { get; set; }
    public TransactionType TransactionType { get; set; }

    public int PaymentMethodTypeId { get; set; }
    public PaymentMethodType PaymentMethodType { get; set; }

    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
}