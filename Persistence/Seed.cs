using Domain;

namespace Persistence;

public class Seed
{
    public static async Task SeedData(DataContext context)
    {
        if (!context.Orders.Any() && !context.OrderItems.Any())
        {
            var items = new List<OrderItem>
            {
                new OrderItem
                {
                    Name = "Pizza",
                    Price = 20,
                    Quantity = 1,
                },
                new OrderItem
                {
                    Name = "Drink",
                    Price = 10,
                    Quantity = 2,
                },
                new OrderItem
                {
                    Name = "Pizza",
                    Price = 30,
                    Quantity = 3,
                },
            };

            var orders = new List<Order>
        {
            new Order
            {
                Table = "P1",
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Name = "Pizza",
                        Price = 20,
                        Quantity = 2,
                    }
                },
                TotalAmount = 40
            },

            new Order
            {
                Table = "P2",
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Name = "Pizza",
                        Price = 40,
                        Quantity = 3,
                    }
                },
                TotalAmount = 120
            },

            new Order
            {
                Table = "K1",
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Name = "Pizza",
                        Price = 50,
                        Quantity = 5,
                    }
                },
                TotalAmount = 250
            }
        };

            await context.Orders.AddRangeAsync(orders);
            await context.SaveChangesAsync();
        }
    }
}
