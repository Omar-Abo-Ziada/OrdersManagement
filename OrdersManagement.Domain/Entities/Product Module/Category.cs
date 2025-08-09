using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.Product_Module;
public class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}