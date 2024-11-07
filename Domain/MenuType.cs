namespace Domain;

// MenuCategory??
public class MenuType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsUsed { get; set; }
    public bool IsDeleted { get; set; }

    public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}