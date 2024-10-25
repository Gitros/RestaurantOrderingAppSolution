namespace Domain;

public class MenuType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}