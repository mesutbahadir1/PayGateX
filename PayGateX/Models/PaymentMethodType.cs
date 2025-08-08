namespace PayGateX.Entities;

public class PaymentMethodType
{
    public int Id { get; set; }
    public string Code { get; set; }           // "ATM"
    public string Description { get; set; }    // "ATM ile Ödeme"

    public List<Transaction> Transactions { get; set; }
}