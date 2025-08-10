using MediatR;
using OrdersManagement.Application.Common.Responses;
using OrdersManagement.Application.Orders.Commands.CreateOrder;
using OrdersManagement.Application.Payments.Commands.ProcessPayment;
using OrdersManagement.Application.Warehouses.Commands.ReserveInventory;
using OrdersManagement.Application.Helpers;

namespace OrdersManagement.Application.Orders.Orchestrators;

public interface IOrderOrchestrator
{
    Task<CustomResultDTO<CreateOrderResponse>> CreateOrderWithPaymentAndInventory(CreateOrderCommand command);
}

public class OrderOrchestrator : IOrderOrchestrator
{
    private readonly IMediator _mediator;
    private readonly ILoggerHelper<OrderOrchestrator> _logger;

    public OrderOrchestrator(IMediator mediator, ILoggerHelper<OrderOrchestrator> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<CustomResultDTO<CreateOrderResponse>> CreateOrderWithPaymentAndInventory(CreateOrderCommand command)
    {
        _logger.LogInformation("Starting order creation orchestration for customer {CustomerId}", command.CustomerId);

        try
        {
            // Step 1: Reserve inventory for all items
            _logger.LogInformation("Step 1: Reserving inventory for order items");
            var reserveInventoryCommand = new ReserveInventoryCommand
            {
                Items = command.OrderItems.Select(item => new ReserveInventoryItemDto
                {
                    DishId = item.DishId,
                    Quantity = item.Quantity
                }).ToList()
            };

            var inventoryResult = await _mediator.Send(reserveInventoryCommand);
            if (!inventoryResult.IsSuccess)
            {
                _logger.LogWarning("Inventory reservation failed: {Message}", inventoryResult.Message);
                return CustomResultDTO<CreateOrderResponse>.Failure(
                    message: "Unable to reserve inventory for the requested items",
                    statusCode: inventoryResult.StatusCode,
                    errors: inventoryResult.Errors
                );
            }

            // Step 2: Create the order
            _logger.LogInformation("Step 2: Creating order");
            var createOrderResult = await _mediator.Send(command);
            if (!createOrderResult.IsSuccess)
            {
                _logger.LogWarning("Order creation failed: {Message}", createOrderResult.Message);
                
                // Rollback: Release reserved inventory
                _logger.LogInformation("Rolling back inventory reservation");
                var releaseInventoryCommand = new ReleaseInventoryCommand
                {
                    ReservationId = inventoryResult.Data!.ReservationId
                };
                await _mediator.Send(releaseInventoryCommand);
                
                return createOrderResult;
            }

            // Step 3: Process payment
            _logger.LogInformation("Step 3: Processing payment for order {OrderId}", createOrderResult.Data!.OrderId);
            var processPaymentCommand = new ProcessPaymentCommand
            {
                OrderId = createOrderResult.Data.OrderId,
                Amount = createOrderResult.Data.TotalAmount,
                PaymentMethod = command.PaymentMethod,
                PaymentToken = command.PaymentToken
            };

            var paymentResult = await _mediator.Send(processPaymentCommand);
            if (!paymentResult.IsSuccess)
            {
                _logger.LogWarning("Payment processing failed for order {OrderId}: {Message}", 
                    createOrderResult.Data.OrderId, paymentResult.Message);
                
                // Note: Order cancellation and inventory release will be handled by the transaction middleware
                // since this is all within a single transaction scope
                
                return CustomResultDTO<CreateOrderResponse>.Failure(
                    message: "Payment processing failed",
                    statusCode: paymentResult.StatusCode,
                    errors: paymentResult.Errors
                );
            }

            _logger.LogInformation("Order orchestration completed successfully for order {OrderId}", 
                createOrderResult.Data.OrderId);

            return createOrderResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during order orchestration for customer {CustomerId}", command.CustomerId);
            
            return CustomResultDTO<CreateOrderResponse>.Failure(
                message: "An error occurred during order processing",
                statusCode: System.Net.HttpStatusCode.InternalServerError,
                errors: new[] { "An unexpected error occurred" }
            );
        }
    }
}
