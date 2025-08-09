using AutoMapper;
using FluentAssertions;
using MyResturants.Application.Resturants.Commands.CreateResturant;
using MyResturants.Domain.Entities;
using Xunit;

namespace MyResturants.Application.Resturants.Dtos.Tests;

public class ResturantsProfileTests
{
    private IMapper mapper;

    public ResturantsProfileTests()
    {

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ResturantsProfile>();
        });

         mapper = mapperConfig.CreateMapper();
    }

    [Fact()]
    public void CreateMap_ForResturantToResturantDto_ShouldMapCorrectly()
    {
        // arrange

        // better to use the config once and resuse it in other tests (store in a  field)
        //var mapperConfig = new MapperConfiguration(cfg =>
        //{
        //    cfg.AddProfile<ResturantsProfile>();
        //});

        //var mapper = mapperConfig.CreateMapper();

        var resturant = new Resturant()
        {
            Id = 1,
            Name = "resturant1",
            Address = new Address()
            {
                Street = "street1",
                City = "city1",
                PostalCode = "postalCode1"
            },
            Category = "category1",
            ContactEmail = "contactEmail1",
            ContactNumber = "contactNumber1",
            Description = "description1",
            HasDelivery = true,
            OwnerId = "1"
        };

        // act
        var resturantDto = mapper.Map<ResturantDto>(resturant);

        // assert

        resturantDto.Should().NotBeNull();
        resturantDto.Id.Should().Be(resturant.Id);
        resturantDto.Name.Should().Be(resturant.Name);
        resturantDto.Street.Should().Be(resturant.Address.Street);
        resturantDto.City.Should().Be(resturant.Address.City);
        resturantDto.PostalCode.Should().Be(resturant.Address.PostalCode);
        resturantDto.Category.Should().Be(resturant.Category);
        resturantDto.Description.Should().Be(resturant.Description);
        resturantDto.HasDelivery.Should().Be(resturant.HasDelivery);
    }

    [Fact()]
    public void CreateMap_ForResturantCommandToResturant_ShouldMapCorrectly()
    {
        // arrange

        //var mapperConfig = new MapperConfiguration(cfg =>
        //{
        //    cfg.AddProfile<ResturantsProfile>();
        //});

        //var mapper = mapperConfig.CreateMapper();

        var resturantCommand = new CreateResturantCommand()
        {
            Name = "resturant1",
            Category = "category1",
            ContactEmail = "contactEmail1",
            ContactNumber = "contactNumber1",
            Description = "description1",
            HasDelivery = true,
        };

        // act
        var resturant = mapper.Map<Resturant>(resturantCommand);

        // assert

        resturant.Should().NotBeNull();
        resturant.Name.Should().Be(resturantCommand.Name);
        resturant.Category.Should().Be(resturantCommand.Category);
        resturant.Description.Should().Be(resturantCommand.Description);
        resturant.HasDelivery.Should().Be(resturantCommand.HasDelivery);
    }
}