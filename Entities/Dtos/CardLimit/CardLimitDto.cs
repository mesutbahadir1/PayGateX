namespace PayGateX.Dtos.CardLimit;

public class CardLimitDto
{
    public int Id { get; set; }
    public decimal TotalLimit { get; set; }
    public decimal UsedLimit { get; set; }

    public decimal AvailableLimit => TotalLimit - UsedLimit;

    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; }
    
    public int CurrencyId { get; set; }
    public int CardId { get; set; }
}