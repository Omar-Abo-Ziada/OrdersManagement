using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace MyResturants.Application.Resturants.Commands.CreateResturant.Tests;

public class CreateResturantCommandValidatorTests
{
    [Fact()]
    public void CreateResturantCommandValidator_ForValidCommand_ShouldNotHaveAnyErrors()
    {
        var command = new CreateResturantCommand()
        {
            Name = "Test",
            Description = "Test",
            Category = "Italian",
            ContactEmail = "Test@Test.com",
            ContactNumber = "12-345",
        };

        var validator = new CreateResturantCommandValidator();

        var result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void CreateResturantCommandValidator_ForInValidCommand_ShouldHaveAnyErrors()
    {
        var command = new CreateResturantCommand()
        {
            Name = "T",
            Category = "Ita",
            ContactEmail = "Test.com",
            PostalCode = "12345"
        };

        var validator = new CreateResturantCommandValidator();

        var result = validator.TestValidate(command);

        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(x => x.Name);
        result.ShouldHaveValidationErrorFor(x => x.Category);
        result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        result.ShouldHaveValidationErrorFor(x => x.PostalCode);
    }

    [Theory]
    [InlineData("Italian")]
    [InlineData("Indian")]
    [InlineData("American")]
    public void CreateResturantCommandValidator_ForValidCategory_ShouldNotHaveAnyCategoryErrors(string category)
    {
        // arrange
        var validator = new CreateResturantCommandValidator();

        var resturant = new CreateResturantCommand()
        {
            Category = category,
        };

        //act
        var result = validator.TestValidate(resturant);

        //assert
        result.ShouldNotHaveValidationErrorFor(x => x.Category);
    }

    [Theory]
    [InlineData("12421")]
    [InlineData("1-45")]
    [InlineData("12- 45")]
    [InlineData("12- 445")]
    [InlineData("123- 45")]
    public void CreateResturantCommandValidator_ForInValidPostalCode_ShouldHavePostalCodeErrors(string postalCode)
    {
        // arrange
        var validator = new CreateResturantCommandValidator();

        var resturant = new CreateResturantCommand()
        {
            PostalCode = postalCode,
        };

        //act
        var result = validator.TestValidate(resturant);

        //assert
        result.ShouldHaveValidationErrorFor(x => x.PostalCode);
    }
}