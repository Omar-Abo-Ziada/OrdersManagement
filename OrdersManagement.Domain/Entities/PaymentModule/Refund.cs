using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.PaymentModule;
public class Refund : BaseEntity
{
    public decimal Amount { get; set; }
    public DateTime RefundDate { get; set; } = DateTime.UtcNow;

    public int PaymentTransactionId { get; set; }
    public PaymentTransaction PaymentTransaction { get; set; } = null!;
}
