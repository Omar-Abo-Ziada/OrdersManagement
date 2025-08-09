using AutoMapper;
using MyResturants.Application.Resturants.Commands.CreateResturant;
using MyResturants.Application.Resturants.Commands.UpdateResturant;
using MyResturants.Domain.Entities;

namespace MyResturants.Application.Resturants.Dtos;

public class ResturantsProfile : Profile
{
    public ResturantsProfile()
    {
        CreateMap<CreateResturantDto, Resturant>()
            .ForMember(dest => dest.Address,
               opt => opt.MapFrom(src => new Address
               {
                   City = src.City ?? string.Empty,
                   Street = src.Street ?? string.Empty,
                   PostalCode = src.PostalCode ?? string.Empty
               }));

        CreateMap<CreateResturantCommand, Resturant>()
           .ForMember(dest => dest.Address,
              opt => opt.MapFrom(src => new Address
              {
                  City = src.City ?? string.Empty,
                  Street = src.Street ?? string.Empty,
                  PostalCode = src.PostalCode ?? string.Empty
              }));

        CreateMap<UpdateResturantCommand, Resturant>()
         .ForMember(dest => dest.Address,
            opt => opt.MapFrom(src => new Address
            {
                City = src.City ?? string.Empty,
                Street = src.Street ?? string.Empty,
                PostalCode = src.PostalCode ?? string.Empty
            }));

        CreateMap<Resturant, ResturantDto>()
            .ForMember(dest => dest.Street,
               opt => opt.MapFrom(src => src.Address != null ? src.Address.Street : null))
            .ForMember(dest => dest.City,
               opt => opt.MapFrom(src => src.Address != null ? src.Address.City : null))
            .ForMember(dest => dest.PostalCode,
               opt => opt.MapFrom(src => src.Address != null ? src.Address.PostalCode : null))
            .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes));
    }
}