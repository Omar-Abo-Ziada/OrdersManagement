using OrdersManagement.Domain.Entities.Order_Module;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.PaymentModule;
public class PaymentInfo : BaseEntity
{
    public string PaymentMethod { get; set; } = null!;
    public bool IsPaid { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
}
