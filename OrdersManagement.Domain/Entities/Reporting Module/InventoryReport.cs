using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.Reporting_Module;
public class InventoryReport : BaseEntity
{
    public DateTime ReportDate { get; set; }
    public int TotalProductsInStock { get; set; }
}