using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class Seed
{
    public static async Task SeedData(RestaurantOrderingContext context)
    {
        if (!context.MenuTypes.Any() && !context.MenuItems.Any() && !context.Tables.Any() &&
            !context.Orders.Any() && !context.OrderItems.Any())
        {
            // Define MenuTypes
            var appetizersType = new MenuType { Id = Guid.NewGuid(), Name = "Appetizers", IsUsed = true, IsDeleted = false };
            var vegPizzaType = new MenuType { Id = Guid.NewGuid(), Name = "Veg Pizza", IsUsed = true, IsDeleted = false };
            var meatPizzaType = new MenuType { Id = Guid.NewGuid(), Name = "Meat Pizza", IsUsed = true, IsDeleted = false };

            // Define MenuItems
            var menuItem1 = new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Garlic Bread",
                Description = "Served with garlic butter and herbs",
                Price = 5.00M,
                IsUsed = true,
                IsDeleted = false,
                MenuTypeId = appetizersType.Id
            };
            var menuItem2 = new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Veggie Delight Pizza",
                Description = "Topped with fresh vegetables and cheese",
                Price = 12.00M,
                IsUsed = true,
                IsDeleted = false,
                MenuTypeId = vegPizzaType.Id
            };
            var menuItem3 = new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Pepperoni Pizza",
                Description = "Topped with pepperoni and extra cheese",
                Price = 15.00M,
                IsUsed = true,
                IsDeleted = false,
                MenuTypeId = meatPizzaType.Id
            };

            // Define Tables
            var table1 = new Table { Id = Guid.NewGuid(), Name = "Table 1", NumberOfPeople = 4, IsOccupied = false, IsUsed = true, IsDeleted = false };
            var table2 = new Table { Id = Guid.NewGuid(), Name = "Table 2", NumberOfPeople = 2, IsOccupied = true, IsUsed = true, IsDeleted = false };

            // Define Orders
            var order1 = new Order
            {
                Id = Guid.NewGuid(),
                OrderDateTime = DateTime.Now.AddHours(-1),
                TotalAmount = menuItem1.Price * 2 + menuItem2.Price,
                OrderStatus = OrderStatus.Pending,
                TableId = table1.Id
            };
            var order2 = new Order
            {
                Id = Guid.NewGuid(),
                OrderDateTime = DateTime.Now.AddHours(-2),
                TotalAmount = menuItem3.Price,
                OrderStatus = OrderStatus.InProgress,
                TableId = table2.Id
            };

            // Define OrderItems
            var orderItem1 = new OrderItem
            {
                Id = Guid.NewGuid(),
                Price = menuItem1.Price,
                Quantity = 2,
                SpecialInstructions = "Extra garlic",
                OrderItemStatus = OrderItemStatus.Pending,
                OrderId = order1.Id,
                MenuItemId = menuItem1.Id
            };
            var orderItem2 = new OrderItem
            {
                Id = Guid.NewGuid(),
                Price = menuItem2.Price,
                Quantity = 1,
                SpecialInstructions = "No onions",
                OrderItemStatus = OrderItemStatus.Pending,
                OrderId = order1.Id,
                MenuItemId = menuItem2.Id
            };
            var orderItem3 = new OrderItem
            {
                Id = Guid.NewGuid(),
                Price = menuItem3.Price,
                Quantity = 1,
                SpecialInstructions = "Extra cheese",
                OrderItemStatus = OrderItemStatus.InProgress,
                OrderId = order2.Id,
                MenuItemId = menuItem3.Id
            };

            // Add entities to context
            await context.MenuTypes.AddRangeAsync(appetizersType, vegPizzaType, meatPizzaType);
            await context.MenuItems.AddRangeAsync(menuItem1, menuItem2, menuItem3);
            await context.Tables.AddRangeAsync(table1, table2);
            await context.Orders.AddRangeAsync(order1, order2);
            await context.OrderItems.AddRangeAsync(orderItem1, orderItem2, orderItem3);

            // Save changes to database
            await context.SaveChangesAsync();
        }
    }
}
