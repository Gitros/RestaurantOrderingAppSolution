using Domain;

namespace Infrastructure.Database;

public class Seed
{
    public static async Task SeedData(RestaurantOrderingContext context)
    {
        if (!context.MenuCategories.Any() && !context.MenuItems.Any() && !context.Tables.Any() &&
            !context.Orders.Any() && !context.OrderItems.Any())
        {
            // Define MenuCategories
            var appetizersCategory = new MenuCategory { Id = Guid.NewGuid(), Name = "Appetizers", IsUsed = true, IsDeleted = false };
            var vegPizzaCategory = new MenuCategory { Id = Guid.NewGuid(), Name = "Veg Pizza", IsUsed = true, IsDeleted = false };
            var meatPizzaCategory = new MenuCategory { Id = Guid.NewGuid(), Name = "Meat Pizza", IsUsed = true, IsDeleted = false };

            // Define MenuItems
            var menuItem1 = new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Garlic Bread",
                Description = "Served with garlic butter and herbs",
                Price = 5.00M,
                IsUsed = true,
                IsDeleted = false,
                MenuCategoryId = appetizersCategory.Id
            };
            var menuItem2 = new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Veggie Delight Pizza",
                Description = "Topped with fresh vegetables and cheese",
                Price = 12.00M,
                IsUsed = true,
                IsDeleted = false,
                MenuCategoryId = vegPizzaCategory.Id
            };
            var menuItem3 = new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = "Pepperoni Pizza",
                Description = "Topped with pepperoni and extra cheese",
                Price = 15.00M,
                IsUsed = true,
                IsDeleted = false,
                MenuCategoryId = meatPizzaCategory.Id
            };

            // Define Ingredients
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Id = Guid.NewGuid(), Name = "Mozzarella", Price = 1.5M, IngredientType = IngredientType.Cheese, IsUsed = true, IsDeleted = false },
                new Ingredient { Id = Guid.NewGuid(), Name = "Cheddar", Price = 1.8M, IngredientType = IngredientType.Cheese, IsUsed = true, IsDeleted = false },
                new Ingredient { Id = Guid.NewGuid(), Name = "Tomato", Price = 0.5M, IngredientType = IngredientType.Vegetables, IsUsed = true, IsDeleted = false },
                new Ingredient { Id = Guid.NewGuid(), Name = "Chicken", Price = 2.5M, IngredientType = IngredientType.Meat, IsUsed = true, IsDeleted = false },
                new Ingredient { Id = Guid.NewGuid(), Name = "Pepperoni", Price = 2.0M, IngredientType = IngredientType.Meat, IsUsed = true, IsDeleted = false }
            };

            // Define Tags
            var tags = new List<Tag>
            {
                new Tag { Id = Guid.NewGuid(), Name = "Spicy", IsUsed = true, IsDeleted = false },
                new Tag { Id = Guid.NewGuid(), Name = "Vegetarian", IsUsed = true, IsDeleted = false },
                new Tag { Id = Guid.NewGuid(), Name = "Gluten-Free", IsUsed = true, IsDeleted = false }
            };

            // Define MenuItemTags
            var menuItemTags = new List<MenuItemTag>
            {
                new MenuItemTag { MenuItemId = menuItem2.Id, TagId = tags[1].Id }, // Veggie Delight Pizza - Vegetarian
                new MenuItemTag { MenuItemId = menuItem3.Id, TagId = tags[0].Id }  // Pepperoni Pizza - Spicy
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

            // Define OrderItemIngredients
            var orderItemIngredients = new List<OrderItemIngredient>
            {
                new OrderItemIngredient { OrderItemId = orderItem1.Id, IngredientId = ingredients[0].Id, Quantity = 1 }, // Extra Mozzarella
                new OrderItemIngredient { OrderItemId = orderItem1.Id, IngredientId = ingredients[1].Id, Quantity = 1 }, // Extra Cheddar
                new OrderItemIngredient { OrderItemId = orderItem3.Id, IngredientId = ingredients[4].Id, Quantity = 2 }  // Extra Pepperoni
            };

            // Add entities to context
            await context.MenuCategories.AddRangeAsync(appetizersCategory, vegPizzaCategory, meatPizzaCategory);
            await context.MenuItems.AddRangeAsync(menuItem1, menuItem2, menuItem3);
            await context.Ingredients.AddRangeAsync(ingredients);
            await context.Tags.AddRangeAsync(tags);
            await context.MenuItemTags.AddRangeAsync(menuItemTags);
            await context.Tables.AddRangeAsync(table1, table2);
            await context.Orders.AddRangeAsync(order1, order2);
            await context.OrderItems.AddRangeAsync(orderItem1, orderItem2, orderItem3);
            await context.OrderItemIngredients.AddRangeAsync(orderItemIngredients);

            // Save changes to database
            await context.SaveChangesAsync();
        }
    }
}