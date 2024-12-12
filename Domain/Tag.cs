namespace Domain;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsUsed { get; set; }
    public bool IsDeleted { get; set; }

    public List<MenuItemTag> MenuItemTags { get; set; } = new List<MenuItemTag>();
}
