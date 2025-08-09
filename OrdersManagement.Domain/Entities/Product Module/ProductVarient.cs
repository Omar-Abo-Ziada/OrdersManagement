using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.Product_Module;
public class ProductVariant : BaseEntity
{
    public string Size { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
