//using FluentValidation;
//using MyResturants.Application.Resturants.Commands.UpdateResturant;

//namespace MyResturants.Application.Resturants.Commands.UpdateResturant;

//public class UpdateResturantCommandValidator : AbstractValidator<UpdateResturantCommand>
//{
//    private readonly List<string> validCategories = ["Italian", "Indian", "American"];

//    public UpdateResturantCommandValidator()
//    {
//        // first way to validate with custom rule
//        RuleFor(p => p.Category)
//           .Must(category => validCategories.Contains(category))
//           .WithMessage($"Category value must be one of {string.Join(" , ", validCategories)}");

//        // second way to validate with custom rule
//        //RuleFor(p => p.Category)
//        //    .Custom((value, context) =>
//        //    {
//        //        if (!validCategories.Contains(value))
//        //        {
//        //            context.AddFailure(nameof(CreateResturantDto.Category), $"Category {value} is not valid , please choose from {string.Join(" , ", validCategories)}");
//        //        }
//        //    });

//        RuleFor(p => p.Name)
//            .Length(3, 100)
//            .WithMessage("Name is required , and it's length should be wihtin 3 to 30 chars");

//        RuleFor(p => p.Description)
//            .NotEmpty()
//            .WithMessage("Description is required.");

//        RuleFor(p => p.Category)
//            .NotEmpty()
//            .WithMessage("Category is required.");

//        RuleFor(p => p.ContactEmail)
//            .EmailAddress()
//            .WithMessage("Invalid Email Address");

//        RuleFor(p => p.ContactNumber)
//            .NotEmpty()
//            .WithMessage("Contact Number is required.");

//        RuleFor(dto => dto.PostalCode)
//            .Matches(@"^\d{2}-\d{3}$")
//            .WithMessage("Please provide a valid postal code (XX-XXX) .");
//    }
//}