using OrdersManagement.Domain.Entities.Order_Module;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.PaymentModule;
public class PaymentTransaction : BaseEntity
{
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public Refund Refund { get; set; } = null!;
}