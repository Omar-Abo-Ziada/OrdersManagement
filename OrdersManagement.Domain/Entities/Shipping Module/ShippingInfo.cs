using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.Entities.Shared;

namespace OrdersManagement.Domain.Entities.Order_Module;
public class ShippingInfo : BaseEntity
{
    public Address Address { get; set; } = new Address();

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
}