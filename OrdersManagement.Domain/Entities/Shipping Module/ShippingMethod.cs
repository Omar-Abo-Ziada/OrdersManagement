namespace OrdersManagement.Domain.Entities.Shipping_Module;
public class ShippingMethod
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Cost { get; set; }
}
