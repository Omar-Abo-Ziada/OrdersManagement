using MediatR;
using OrdersManagement.Application.Common.Responses;

namespace OrdersManagement.Application.Orders.Queries.GetOrder;

public class GetOrderQuery : IRequest<CustomResultDTO<OrderDetailsResponse>>
{
    public int OrderId { get; set; }
    public int? CustomerId { get; set; } // For customer-specific access control
}

public class OrderDetailsResponse
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    
    // Shipping Information
    public ShippingAddressDto ShippingAddress { get; set; } = new();
    
    // Payment Information
    public PaymentDetailsDto? Payment { get; set; }
    
    // Shipment Information
    public ShipmentDetailsDto? Shipment { get; set; }
    
    // Order Items
    public List<OrderItemDetailsDto> OrderItems { get; set; } = new();
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Notes { get; set; }
}

public class ShippingAddressDto
{
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public class PaymentDetailsDto
{
    public int Id { get; set; }
    public string PaymentNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? ProcessedAt { get; set; }
    public string? CardLastFourDigits { get; set; }
    public string? CardType { get; set; }
}

public class ShipmentDetailsDto
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public string CarrierName { get; set; } = string.Empty;
    public string? CarrierTrackingNumber { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
}

public class OrderItemDetailsDto
{
    public int Id { get; set; }
    public int DishId { get; set; }
    public string DishName { get; set; } = string.Empty;
    public string? DishDescription { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public string? SpecialInstructions { get; set; }
    public string? WarehouseName { get; set; }
}
