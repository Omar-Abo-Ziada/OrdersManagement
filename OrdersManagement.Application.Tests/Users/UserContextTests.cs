using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using MyResturants.Domain.Constants;
using System.Security.Claims;

namespace MyResturants.Application.Users.Tests;

[TestClass()]
public class UserContextTests
{
    [TestMethod()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        // arrange
        var dateOfBirth = new DateOnly(2000, 1, 1);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Email, "test@test.com"),
            new Claim(ClaimTypes.Role, UserRoles.Admin),
            new Claim(ClaimTypes.Role, UserRoles.User),
            new Claim("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd")),
            new Claim("Nationality", "Egyptian")
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims , "test"));

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextAccessorMock.Object);

        // act
        var result = userContext.GetCurrentUser();

        // assert
        result.Should().NotBeNull();
        result!.Id.Should().Be("1");
        result.Email.Should().Be("test@test.com");
        result.Roles.Should().ContainInOrder(UserRoles.Admin);
        result.Roles.Should().ContainInOrder(UserRoles.User);
        result.DateOfBirth.Should().Be(dateOfBirth);
        result.Nationality.Should().Be("Egyptian");
    }

    [TestMethod()]
    public void GetCurrentUser_WithUserContextNotPresent_ShouldThrowInvalidOperationException()
    {
        // arrange
        var dateOfBirth = new DateOnly(2000, 1, 1);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

        var userContext = new UserContext(httpContextAccessorMock.Object);

        // act
        Action action = () => userContext.GetCurrentUser();

        // assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("User Context doesn't exist");
    }

}
