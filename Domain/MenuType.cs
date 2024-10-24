namespace Domain;

public class MenuType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<MenuItem> MenuItems { get; set; }
}