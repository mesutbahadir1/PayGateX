namespace PayGateX.Entities;

public class Currency
{
    public int Id { get; set; }
    public string Code { get; set; }           // "TRY"
    public string Description { get; set; }    // "Türk Lirası"

    public List<CardLimit> CardLimits { get; set; }
    public List<Transaction> Transactions { get; set; }
}