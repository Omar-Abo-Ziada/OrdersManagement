using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using MyResturants.Application.Users;
using MyResturants.Domain.Constants;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace MyResturants.Application.Resturants.Commands.CreateResturant.Tests;

public class CreateResturantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedResturantId()
    {

        // arrange
        var loggerMock = new Mock<ILogger<CreateResturantCommandHandler>>();

        var createResturantCommand = new CreateResturantCommand()
        {
            Name = "test",
            Description = "test",
            Category = "test",
            ContactEmail = "test",
            ContactNumber = "test",
            PostalCode = "test",
            City = "test",
            Street = "test",
            HasDelivery = true
        };
        var mapperMock = new Mock<IMapper>();

        var resturant = new  Resturant();

        mapperMock.Setup(m => m.Map<Resturant>(createResturantCommand)).Returns(resturant);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.User]);
        userContextMock.Setup(ctx => ctx.GetCurrentUser()).Returns(currentUser);

        var resturantRepoMock = new Mock<IResturantRepository>();

        resturantRepoMock.Setup(repo => repo.CreateAsync(It.IsAny<Resturant>())).ReturnsAsync(1);

        var createResturantCommandHander =
            new CreateResturantCommandHandler(loggerMock.Object, mapperMock.Object,
                                               userContextMock.Object, resturantRepoMock.Object);

        // act
        var result = await createResturantCommandHander.Handle(createResturantCommand, CancellationToken.None);

        // assert
        result.Should().Be(1);

        resturantRepoMock.Verify(repo => repo.CreateAsync(It.IsAny<Resturant>()), Times.Once);

        resturant.OwnerId.Should().Be(currentUser.Id);

        userContextMock.Verify(ctx => ctx.GetCurrentUser(), Times.Once);

        mapperMock.Verify(mapper => mapper.Map<Resturant>(It.IsAny<CreateResturantCommand>()), Times.Once);
    }
}