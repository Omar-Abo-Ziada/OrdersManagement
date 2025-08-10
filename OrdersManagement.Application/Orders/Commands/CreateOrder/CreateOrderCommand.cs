using MediatR;
using OrdersManagement.Application.Common.Responses;

namespace OrdersManagement.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<CustomResultDTO<CreateOrderResponse>>
{
    public int CustomerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = new();
    
    // Shipping Information
    public string ShippingAddress { get; set; } = string.Empty;
    public string ShippingCity { get; set; } = string.Empty;
    public string ShippingState { get; set; } = string.Empty;
    public string ShippingZipCode { get; set; } = string.Empty;
    public string ShippingCountry { get; set; } = string.Empty;
    
    // Payment Information
    public string PaymentMethod { get; set; } = string.Empty;
    public string? PaymentToken { get; set; }
    
    public string? Notes { get; set; }
}

public class OrderItemDto
{
    public int DishId { get; set; }
    public int Quantity { get; set; }
    public string? SpecialInstructions { get; set; }
}

public class CreateOrderResponse
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
}
