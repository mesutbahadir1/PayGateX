namespace PayGateX.Entities;

public class ProductType
{
    public int Id { get; set; }
    public string Code { get; set; }           // "BONUS"
    public string Description { get; set; }    // "Bonuslu Kart"

    public List<Card> Cards { get; set; }

}