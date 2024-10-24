using Domain;

namespace Domain;

public class MenuItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Guid MenuTypeId { get; set; }
    public MenuType MenuType { get; set; }
}
