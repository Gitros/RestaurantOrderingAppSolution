using Domain;

namespace Infrastructure.Database;

public class Seed
{
    public static async Task SeedData(RestaurantOrderingContext context)
    {
        if (!context.Tables.Any())
        {
            //var tables = new List<Table>
            //{
            //    new Table
            //    {
            //        TableId = 1,
            //        TableNumber = "P1"
            //    },
            //    new Table
            //    {
            //        TableId = 2,
            //        TableNumber = "P2"
            //    },
            //    new Table
            //    {
            //        TableId = 3,
            //        TableNumber = "L1"
            //    },
            //    new Table
            //    {
            //        TableId = 4,
            //        TableNumber = "L2"
            //    },
            //    new Table
            //    {
            //        TableId = 5,
            //        TableNumber = "K1"
            //    },
            //    new Table
            //    {
            //        TableId = 6,
            //        TableNumber = "K2"
            //    },
            //    new Table
            //    {
            //        TableId = 7,
            //        TableNumber = "K3"
            //    },
            //    new Table
            //    {
            //        TableId = 8,
            //        TableNumber = "K4"
            //    },
            //    new Table
            //    {
            //        TableId = 9,
            //        TableNumber = "B1"
            //    },
            //    new Table
            //    {
            //        TableId = 10,
            //        TableNumber = "B2"
            //    },
            //    new Table
            //    {
            //        TableId = 11,
            //        TableNumber = "B3"
            //    }
            //};

            //await context.Tables.AddRangeAsync(tables);
            //await context.SaveChangesAsync();
        }
    }
}
