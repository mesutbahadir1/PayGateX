namespace PayGateX.Entities;

public class CardStatus
{
    public int Id { get; set; }
    public string Code { get; set; }           // "ACTIVE"
    public string Description { get; set; }    // "Aktif Kart"

    public List<Card> Cards { get; set; }

}