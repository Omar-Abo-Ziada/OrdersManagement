using OrdersManagement.Domain.Entities.Order_Module;
using OrdersManagement.Domain.Entities.Warehouse_Module;

namespace OrdersManagement.Domain.Entities.Shipment_Module;

public class Shipment
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;
    
    public int? WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }
    
    public ShipmentStatus Status { get; set; }
    public ShippingMethod Method { get; set; }
    
    // Carrier Information
    public string CarrierName { get; set; } = string.Empty;
    public string? CarrierTrackingNumber { get; set; }
    public decimal ShippingCost { get; set; }
    
    // Address Information
    public string ShippingAddress { get; set; } = string.Empty;
    public string ShippingCity { get; set; } = string.Empty;
    public string ShippingState { get; set; } = string.Empty;
    public string ShippingZipCode { get; set; } = string.Empty;
    public string ShippingCountry { get; set; } = string.Empty;
    
    // Recipient Information
    public string RecipientName { get; set; } = string.Empty;
    public string? RecipientPhone { get; set; }
    public string? RecipientEmail { get; set; }
    
    // Dates
    public DateTime? ShippedAt { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    
    // Package Information
    public decimal? Weight { get; set; }
    public string? WeightUnit { get; set; }
    public string? Dimensions { get; set; }
    public int PackageCount { get; set; } = 1;
    
    // Tracking Events
    public ICollection<ShipmentTracking> TrackingEvents { get; set; } = new List<ShipmentTracking>();
    
    // Audit
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Notes { get; set; }
}

public enum ShipmentStatus
{
    Created = 1,
    PickedUp = 2,
    InTransit = 3,
    OutForDelivery = 4,
    Delivered = 5,
    Failed = 6,
    Returned = 7,
    Cancelled = 8
}

public enum ShippingMethod
{
    Standard = 1,
    Express = 2,
    Overnight = 3,
    TwoDay = 4,
    SameDay = 5,
    Pickup = 6
}
