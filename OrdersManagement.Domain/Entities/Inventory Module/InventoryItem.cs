using OrdersManagement.Domain.Entities.Product_Module;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.Inventory_Module;
public class InventoryItem : BaseEntity
{
    // Removed Id, now coming from BaseEntity
    public int QuantityAvailable { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
}

