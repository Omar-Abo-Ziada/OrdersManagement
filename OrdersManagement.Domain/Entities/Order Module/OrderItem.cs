using OrdersManagement.Domain.Entities.Product_Module;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.Order_Module;
public class OrderItem : BaseEntity
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
