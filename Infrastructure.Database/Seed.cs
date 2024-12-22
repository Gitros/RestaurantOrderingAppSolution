using Domain;

namespace Infrastructure.Database;

public class Seed
{
    public static async Task SeedData(RestaurantOrderingContext context)
    {
        if (!context.MenuCategories.Any())
        {
            // MenuCategories
            var menuCategories = new List<MenuCategory>
        {
            new MenuCategory { Id = Guid.NewGuid(), Name = "Appetizers", IsUsed = true, IsDeleted = false },
            new MenuCategory { Id = Guid.NewGuid(), Name = "Veg Pizza", IsUsed = true, IsDeleted = false }
        };
            await context.MenuCategories.AddRangeAsync(menuCategories);

            // MenuItems
            var menuItems = new List<MenuItem>
        {
            new MenuItem { Id = Guid.NewGuid(), Name = "Garlic Bread", Price = 5.00M, IsUsed = true, IsDeleted = false, MenuCategoryId = menuCategories[0].Id },
            new MenuItem { Id = Guid.NewGuid(), Name = "Veggie Pizza", Price = 12.00M, IsUsed = true, IsDeleted = false, MenuCategoryId = menuCategories[1].Id }
        };
            await context.MenuItems.AddRangeAsync(menuItems);

            // Ingredients
            var ingredients = new List<Ingredient>
        {
            new Ingredient { Id = Guid.NewGuid(), Name = "Mozzarella", Price = 1.5M, IngredientType = IngredientType.Cheese, IsUsed = true, IsDeleted = false }
        };
            await context.Ingredients.AddRangeAsync(ingredients);

            // Tags
            var tags = new List<Tag>
        {
            new Tag { Id = Guid.NewGuid(), Name = "Vegetarian", IsUsed = true, IsDeleted = false }
        };
            await context.Tags.AddRangeAsync(tags);

            // Tables
            var tables = new List<Table>
        {
            new Table { Id = Guid.NewGuid(), Name = "Table 1", NumberOfPeople = 4, IsOccupied = false, IsUsed = true, IsDeleted = false }
        };
            await context.Tables.AddRangeAsync(tables);

            // Orders
            var orders = new List<Order>
        {
            new Order
            {
                Id = Guid.NewGuid(),
                OrderDateTime = DateTime.UtcNow,
                OrderStatus = OrderStatus.Pending,
                OrderType = OrderType.DineIn,
                PaymentMethod = PaymentMethod.Card,
                TableId = tables[0].Id
            }
        };
            await context.Orders.AddRangeAsync(orders);

            // OrderItems
            var orderItems = new List<OrderItem>
        {
            new OrderItem
            {
                Id = Guid.NewGuid(),
                MenuItemId = menuItems[0].Id,
                OrderId = orders[0].Id,
                Quantity = 1,
                Price = 5.00M,
                SpecialInstructions = "Extra Cheese",
                OrderItemStatus = OrderItemStatus.Pending
            }
        };
            await context.OrderItems.AddRangeAsync(orderItems);

            // OrderItemIngredients
            var orderItemIngredients = new List<OrderItemIngredient>
        {
            new OrderItemIngredient { OrderItemId = orderItems[0].Id, IngredientId = ingredients[0].Id, Quantity = 1 }
        };
            await context.OrderItemIngredients.AddRangeAsync(orderItemIngredients);

            await context.SaveChangesAsync();
        }
    }

}
