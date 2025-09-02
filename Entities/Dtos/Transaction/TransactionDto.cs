namespace PayGateX.Dtos.Transaction;

public class TransactionDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }= DateTime.UtcNow;

    public int CardId { get; set; }

    public string AppUserId { get; set; }

    public int TransactionTypeId { get; set; }

    public int PaymentMethodTypeId { get; set; }

    public int CurrencyId { get; set; }
}