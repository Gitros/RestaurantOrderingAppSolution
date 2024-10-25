using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<MenuType> Menus { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Table> Tables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<MenuType>()
            .HasMany(m => m.MenuItems)
            .WithOne(mi => mi.MenuType)
            .HasForeignKey(mi => mi.MenuTypeId);

        modelBuilder.Entity<Table>()
            .HasMany(t => t.Orders)
            .WithOne(o => o.Table)
            .HasForeignKey(o => o.TableId);
    }
}
