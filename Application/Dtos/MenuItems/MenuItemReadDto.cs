namespace Application.Dtos.MenuItems;

public class MenuItemReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsUsed { get; set; }
    public bool IsDeleted { get; set; }

    public Guid MenuTypeId { get; set; }
    public string MenuTypeName { get; set; }
}
