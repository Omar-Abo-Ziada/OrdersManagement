namespace OrdersManagement.Domain.Entities.Shipment_Module;

public class ShipmentTracking
{
    public int Id { get; set; }
    public int ShipmentId { get; set; }
    public Shipment Shipment { get; set; } = default!;
    
    public DateTime EventDate { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Location { get; set; }
    
    public string? CarrierEventCode { get; set; }
    public string? CarrierEventDescription { get; set; }
    
    public DateTime CreatedAt { get; set; }
}
