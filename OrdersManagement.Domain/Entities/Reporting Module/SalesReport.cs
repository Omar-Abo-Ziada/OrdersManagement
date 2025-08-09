using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.Reporting_Module;
public class SalesReport : BaseEntity
{
    public DateTime ReportDate { get; set; }
    public decimal TotalSales { get; set; }
}