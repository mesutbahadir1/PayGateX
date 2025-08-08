namespace PayGateX.Entities;

public class TransactionType
{
    public int Id { get; set; }
    public string Code { get; set; }           // "SALE"
    public string Description { get; set; }    // "Satış İşlemi"

    public List<Transaction> Transactions { get; set; }
}