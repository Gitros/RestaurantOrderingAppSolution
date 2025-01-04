namespace Domain;

public class CustomerInformation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string PhoneNumber { get; set; } = null!;
    public string? AdditionalInstructions { get; set; }
    public string? Address { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
}
