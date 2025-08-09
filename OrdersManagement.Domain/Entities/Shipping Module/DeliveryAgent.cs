using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.Shipping_Module;
public class DeliveryAgent : BaseEntity
{
    public string Name { get; set; } = null!;
    public string PhoneNumber { get; set; } = string.Empty;

    public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
}
