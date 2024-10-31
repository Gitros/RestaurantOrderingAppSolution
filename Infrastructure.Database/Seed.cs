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
            var przystawkiType = new MenuType { Id = Guid.NewGuid(), Name = "Przystawki" };
            var pizzaVegeType = new MenuType { Id = Guid.NewGuid(), Name = "Pizza Vege" };
            var pizzaSzynkaType = new MenuType { Id = Guid.NewGuid(), Name = "Pizza z Szynką / Szynką Parmeńską" };
            var pizzaSalamiType = new MenuType { Id = Guid.NewGuid(), Name = "Pizza z Salami / Salami Picante" };

            // Define MenuItems
            var menuItem1 = new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Podplomyki 1",
                Description = "ser, oliwa czosnkowa, rukola",
                Price = 20.00M,
                MenuTypeId = przystawkiType.Id
            };
            var menuItem2 = new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Pizza Vege 1",
                Description = "Sos pomidorowy, mozzarella, bazylia",
                Price = 30.00M,
                MenuTypeId = pizzaVegeType.Id
            };
            var menuItem3 = new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Pizza Szynka 1",
                Description = "Sos pomidorowy, mozzarella, szynka parmeńska, pomidorki koktajlowe, parmezan, rukola",
                Price = 42.00M,
                MenuTypeId = pizzaSzynkaType.Id
            };

            // Define Tables
            var table1 = new Table { Id = Guid.NewGuid(), Name = "Table 1", NumberOfPeople = 4, IsOccupied = false };
            var table2 = new Table { Id = Guid.NewGuid(), Name = "Table 2", NumberOfPeople = 2, IsOccupied = true };

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
                SpecialInstructions = "Extra cheese",
                OrderId = order1.Id,
                MenuItemId = menuItem1.Id
            };
            var orderItem2 = new OrderItem
            {
                Id = Guid.NewGuid(),
                Price = menuItem2.Price,
                Quantity = 1,
                SpecialInstructions = "No basil",
                OrderId = order1.Id,
                MenuItemId = menuItem2.Id
            };
            var orderItem3 = new OrderItem
            {
                Id = Guid.NewGuid(),
                Price = menuItem3.Price,
                Quantity = 1,
                SpecialInstructions = "Extra arugula",
                OrderId = order2.Id,
                MenuItemId = menuItem3.Id
            };

            // Add entities to context
            await context.MenuTypes.AddRangeAsync(przystawkiType, pizzaVegeType, pizzaSzynkaType, pizzaSalamiType);
            await context.MenuItems.AddRangeAsync(menuItem1, menuItem2, menuItem3);
            await context.Tables.AddRangeAsync(table1, table2);
            await context.Orders.AddRangeAsync(order1, order2);
            await context.OrderItems.AddRangeAsync(orderItem1, orderItem2, orderItem3);

            // Save changes to database
            await context.SaveChangesAsync();
        }
    }
}
