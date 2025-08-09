using FluentValidation;

namespace MyResturants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Description)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Price)
            //.NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price Must be a non-negative number");

        RuleFor(v => v.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price Must be a non-negative number");
    }
}