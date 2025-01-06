﻿namespace Application.Dtos.MenuItems;

public class MenuItemDetailedDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsUsed { get; set; }
    public bool IsDeleted { get; set; }

    public Guid MenuCategoryId { get; set; }
    public string MenuCategoryName { get; set; } = null!;
}
