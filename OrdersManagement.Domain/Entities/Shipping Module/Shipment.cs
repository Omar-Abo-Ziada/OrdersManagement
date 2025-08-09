using OrdersManagement.Domain.Entities.Order_Module;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.Shipping_Module;
public class Shipment : BaseEntity
{
    public DateTime ShippedDate { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int DeliveryAgentId { get; set; }
    public DeliveryAgent DeliveryAgent { get; set; } = null!;
}
