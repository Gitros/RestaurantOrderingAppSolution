using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class RestaurantOrderingContext : DbContext
{
    public RestaurantOrderingContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<MenuCategory> MenuCategories { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<MenuItemTag> MenuItemTags { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<OrderItemIngredient> OrderItemIngredients { get; set; }
    public DbSet<DeliveryInformation> DeliveryInformation { get; set; }
    public DbSet<TakeawayInformation> TakeawayInformation { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Order and OrderItem relationship
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);

        // One-to-One: Order and DeliveryInformation
        modelBuilder.Entity<Order>()
            .HasOne(o => o.DeliveryInformation)
            .WithOne(di => di.Order)
            .HasForeignKey<DeliveryInformation>(di => di.OrderId);

        // One-to-One: Order and TakeawayInformation
        modelBuilder.Entity<Order>()
            .HasOne(o => o.TakeawayInformation)
            .WithOne(ti => ti.Order)
            .HasForeignKey<TakeawayInformation>(ti => ti.OrderId);

        // OrderItem and MenuItem relationship
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.MenuItem)
            .WithMany()
            .HasForeignKey(oi => oi.MenuItemId);

        // MenuCategory and MenuItem relationship
        modelBuilder.Entity<MenuCategory>()
            .HasMany(mc => mc.MenuItems)
            .WithOne(mi => mi.MenuCategory)
            .HasForeignKey(mi => mi.MenuCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // MenuItem and MenuCategory relationship
        modelBuilder.Entity<MenuItem>()
            .HasOne(mi => mi.MenuCategory)
            .WithMany(mc => mc.MenuItems)
            .HasForeignKey(mi => mi.MenuCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Table and Order relationship
        modelBuilder.Entity<Table>()
            .HasMany(t => t.Orders)
            .WithOne(o => o.Table)
            .HasForeignKey(o => o.TableId)
            .OnDelete(DeleteBehavior.Restrict);

        // MenuItem and Tag (Many-to-Many)
        modelBuilder.Entity<MenuItemTag>()
            .HasKey(mt => new { mt.MenuItemId, mt.TagId });

        modelBuilder.Entity<MenuItemTag>()
            .HasOne(mt => mt.MenuItem)
            .WithMany(mi => mi.MenuItemTags)
            .HasForeignKey(mt => mt.MenuItemId);

        modelBuilder.Entity<MenuItemTag>()
           .HasOne(mt => mt.Tag)
           .WithMany(t => t.MenuItemTags)
           .HasForeignKey(mt => mt.TagId);

        // OrderItem and Ingredient (Many-to-Many)
        modelBuilder.Entity<OrderItemIngredient>()
            .HasKey(oii => new { oii.OrderItemId, oii.IngredientId });

        modelBuilder.Entity<OrderItemIngredient>()
            .HasOne(oii => oii.OrderItem)
            .WithMany(oi => oi.OrderItemIngredients)
            .HasForeignKey(oii => oii.OrderItemId);

        modelBuilder.Entity<OrderItemIngredient>()
            .HasOne(oii => oii.Ingredient)
            .WithMany(i => i.OrderItemIngredients)
            .HasForeignKey(oii => oii.IngredientId);
    }
}