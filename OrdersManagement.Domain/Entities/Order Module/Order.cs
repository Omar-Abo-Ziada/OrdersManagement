
using OrdersManagement.Domain.Entities.PaymentModule;
using OrdersManagement.Domain.Entities.User_Module;
using System.ComponentModel.DataAnnotations.Schema;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.Order_Module;

// TODO : Add Schemas To Entities
[Table( name:"Orders" , Schema = "Orders")]
public class Order : BaseEntity
{
    // Removed Id and CreatedAt, now coming from BaseEntity
    public OrderStatus Status { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    public PaymentInfo PaymentInfo { get; set; } = null!;
    public ShippingInfo ShippingInfo { get; set; } = null!;
}