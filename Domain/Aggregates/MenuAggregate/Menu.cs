namespace Domain.Aggregates.MenuAggregate;

public class Menu
{
    public int MenuId { get; set; }
    public string MenuName { get; set; }
    public List<MenuItem> MenuItems { get; set; }
}
