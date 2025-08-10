using FluentValidation;

namespace OrdersManagement.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .WithMessage("Customer ID is required");

        RuleFor(x => x.OrderItems)
            .NotEmpty()
            .WithMessage("Order must contain at least one item");

        RuleForEach(x => x.OrderItems)
            .SetValidator(new OrderItemDtoValidator());

        RuleFor(x => x.ShippingAddress)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Shipping address is required and must not exceed 500 characters");

        RuleFor(x => x.ShippingCity)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Shipping city is required and must not exceed 100 characters");

        RuleFor(x => x.ShippingState)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Shipping state is required and must not exceed 100 characters");

        RuleFor(x => x.ShippingZipCode)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("Shipping zip code is required and must not exceed 20 characters");

        RuleFor(x => x.ShippingCountry)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Shipping country is required and must not exceed 100 characters");

        RuleFor(x => x.PaymentMethod)
            .NotEmpty()
            .WithMessage("Payment method is required");

        RuleFor(x => x.Notes)
            .MaximumLength(1000)
            .WithMessage("Notes must not exceed 1000 characters");
    }
}

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(x => x.DishId)
            .GreaterThan(0)
            .WithMessage("Dish ID is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.SpecialInstructions)
            .MaximumLength(500)
            .WithMessage("Special instructions must not exceed 500 characters");
    }
}
