using OrdersManagement.Domain.Entities.Inventory_Module;
using OrdersManagement.Domain.Entities.Order_Module;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.Product_Module;
public class Product : BaseEntity
{
    // Removed Id, now coming from BaseEntity
    public string Name { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // Navigation
    public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}