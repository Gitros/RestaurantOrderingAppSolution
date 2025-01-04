namespace Application.Dtos.CustomerInformations;

public class CustomerInformationReadDto
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string AdditionalInstructions { get; set; }
    public string Address { get; set; }
}
