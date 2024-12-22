namespace Domain;

public class DeliveryInformation
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string DeliveryInstructions { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}
