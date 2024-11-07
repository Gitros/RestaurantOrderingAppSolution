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
    public DbSet<MenuType> MenuTypes { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Table> Tables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Global Query Filters for Soft Delete
        modelBuilder.Entity<Order>().HasQueryFilter(o => !o.IsDeleted);
        modelBuilder.Entity<OrderItem>().HasQueryFilter(oi => !oi.IsDeleted);
        modelBuilder.Entity<MenuType>().HasQueryFilter(mt => !mt.IsDeleted);
        modelBuilder.Entity<MenuItem>().HasQueryFilter(mi => !mi.IsDeleted);
        modelBuilder.Entity<Table>().HasQueryFilter(t => !t.IsDeleted);

        // Define relationships with Restrict DeleteBehavior to prevent cascading deletions
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MenuType>()
            .HasMany(m => m.MenuItems)
            .WithOne(mi => mi.MenuType)
            .HasForeignKey(mi => mi.MenuTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Table>()
            .HasMany(t => t.Orders)
            .WithOne(o => o.Table)
            .HasForeignKey(o => o.TableId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.MenuItem)
            .WithMany()
            .HasForeignKey(oi => oi.MenuItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
