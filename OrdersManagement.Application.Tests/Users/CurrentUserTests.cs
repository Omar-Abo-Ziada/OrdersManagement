using FluentAssertions;
using MyResturants.Domain.Constants;
using Xunit;

namespace MyResturants.Application.Users.Tests;

public class CurrentUserTests
{
    // TestMehtod_Scenario_ExpectedResult
    //[Fact()]
    //public void IsInRole_WithMatchingRole_ShouldReturnTrue()
    //{
    //    // arrange
    //    var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin , UserRoles.User] , null , null);

    //    // act
    //    var result = currentUser.IsInRole(UserRoles.Admin);

    //    // assert
    //    result.Should().BeTrue();
    //}

    // or i can gather test cases in one method
    [Theory]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string role)
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act
        var result = currentUser.IsInRole(role);

        // assert
        result.Should().BeTrue();
    }



    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act
        var result = currentUser.IsInRole(UserRoles.Owner);

        // assert
        result.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act
        var result = currentUser.IsInRole(UserRoles.Admin.ToLower());

        // assert
        result.Should().BeFalse();
    }


   
}