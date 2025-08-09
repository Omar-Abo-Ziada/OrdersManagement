using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Entities.Shared;

namespace OrdersManagement.Domain.Entities.Inventory_Module;
public class Warehouse : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;

    public Address Address { get; set; } = new Address();

    public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
}