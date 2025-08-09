using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Exceptions;
using MyResturants.Domain.Repositories;
using Xunit;

namespace MyResturants.Application.Resturants.Commands.UpdateResturant.Tests
{
    public class UpdateResturantCommandHandlerTests
    {
        private readonly Mock<ILogger<UpdateResturantCommandHandler>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IResturantRepository> _resturantRepoMock;
        private readonly UpdateResturantCommandHandler _handler;

        public UpdateResturantCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateResturantCommandHandler>>();
            _mapperMock = new Mock<IMapper>();
            _resturantRepoMock = new Mock<IResturantRepository>();
            _handler = new UpdateResturantCommandHandler(_loggerMock.Object, _mapperMock.Object, _resturantRepoMock.Object);
        }

        [Fact]
        public async Task Handle_WhenResturantNotFound_ShouldThrowCustomNotFoundException()
        {
            // arrange
            var updateResturantCommand = new UpdateResturantCommand(1);

            _resturantRepoMock.Setup(repo => repo.GetByIdAsync(updateResturantCommand.Id))
                .ReturnsAsync((Resturant)null);

            // act
            Func<Task> action = async () => await _handler.Handle(updateResturantCommand, CancellationToken.None);

            // assert
            await action.Should().ThrowAsync<CustomNotFoundException>();
        }

        [Fact]
        public async Task Handle_WhenResturantCommandIsValid_ShouldUpdateResturant()
        {
            // arrange
            var existingResturant = new Resturant { Id = 1 };
            var updateResturantCommand = new UpdateResturantCommand(1);

            _resturantRepoMock.Setup(repo => repo.GetByIdAsync(updateResturantCommand.Id))
                .ReturnsAsync(existingResturant);

            // act
            await _handler.Handle(updateResturantCommand, CancellationToken.None);

            // assert
            _resturantRepoMock.Verify(r => r.UpdateAsync(existingResturant), Times.Once);
            _mapperMock.Verify(m => m.Map(updateResturantCommand, existingResturant), Times.Once);
        }
    }
}
