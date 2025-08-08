namespace PayGateX.Entities;

public class CardType
{
    public int Id { get; set; }
    public string Code { get; set; }           // "VISA"
    public string Description { get; set; }    // "Visa Kart"

    public List<Card> Cards { get; set; }

}