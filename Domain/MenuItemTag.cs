namespace Domain;

public class MenuItemTag
{
    public Guid MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }

    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}
