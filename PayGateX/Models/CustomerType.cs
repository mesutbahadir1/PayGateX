namespace PayGateX.Entities;

public class CustomerType
{
    public int Id { get; set; }
    public string Code { get; set; }           // "VIP"
    public string Description { get; set; }    // "Vip Müşteri"

    public List<Customer> Customers { get; set; }

}