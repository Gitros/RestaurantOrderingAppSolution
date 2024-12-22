namespace Domain;

public class TakeawayInformation
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string AdditionalInstructions { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}
