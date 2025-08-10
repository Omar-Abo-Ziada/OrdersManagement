using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Entities.Inventory_Module;
using OrdersManagement.Domain.Entities.Order_Module;
using OrdersManagement.Domain.Entities.PaymentModule;
using OrdersManagement.Domain.Entities.Product_Module;
using OrdersManagement.Domain.Entities.Reporting_Module;
using OrdersManagement.Domain.Entities.Shipping_Module;
using OrdersManagement.Domain.Entities.User_Module;

namespace OrdersManagement.Infrastructure.Presistance;

public class ApplicationDbContext : IdentityDbContext<User , IdentityRole<int> , int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; // This will make all queries to be NoTracking by default, which is good for read-only operations
    }

    // Inventory Module
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }

    // Order Module
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    // Payment Module
    public DbSet<PaymentInfo> PaymentInfos { get; set; }
    public DbSet<Refund> Refunds { get; set; }
    public DbSet<PaymentTransaction> paymentTransactions { get; set; }

    // Product Module
    public DbSet<Category>  Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }

    // Reporting Module
    public DbSet<InventoryReport> InventoryReports { get; set; }
    public DbSet<OrderPerformance> OrderPerformances { get; set; }
    public DbSet<SalesReport> SalesReports { get; set; }

    // Shipping Module
    public DbSet<DeliveryAgent> DeliveryAgents { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<ShippingInfo> ShippingInfos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Leave it Very Imortant or u will face issues like 'The entity type 'IdentityUserLogin<string>' requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating

        // Owned types configuration
        modelBuilder.Entity<ShippingInfo>().OwnsOne(s => s.Address);
        modelBuilder.Entity<Warehouse>().OwnsOne(s => s.Address);
    }

    public override int SaveChanges()
    {
        ApplyTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyTimestamps()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        var utcNow = DateTime.UtcNow;
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity.CreatedAt == default)
                {
                    entry.Entity.CreatedAt = utcNow;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = utcNow;
            }
        }
    }
}