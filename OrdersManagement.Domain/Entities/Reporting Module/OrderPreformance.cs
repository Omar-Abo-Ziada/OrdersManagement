using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Domain.Entities.Reporting_Module;
public class OrderPerformance : BaseEntity
{
    public DateTime ReportDate { get; set; }
    public int OrdersCompleted { get; set; }
    public int OrdersCancelled { get; set; }
}
