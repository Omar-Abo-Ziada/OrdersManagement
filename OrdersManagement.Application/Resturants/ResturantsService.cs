using AutoMapper;
using Microsoft.Extensions.Logging;
using MyResturants.Application.Resturants.Dtos;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Repositories;

namespace MyResturants.Application.Resturants;

internal class ResturantsService(IResturantRepository resturantRepository , 
    ILogger<ResturantsService> logger , IMapper mapper) 
    : IResturantsService
{
    public async Task<int> CreateAsync(CreateResturantDto createResturantDto)
    {
        logger.LogInformation("Creating New Resturant");
        var resturant = mapper.Map<Resturant>(createResturantDto);
        int id = await resturantRepository.CreateAsync(resturant);
        return id;
    }

    public async Task<IEnumerable<ResturantDto>> GetAllAsync()
    {
        logger.LogInformation("Getting All Resturants");
        var resturants = await resturantRepository.GetAllAsync();
        var resturantDtos = mapper.Map<IEnumerable<ResturantDto>>(resturants);
        return resturantDtos;
    }

    public async Task<ResturantDto?> GetByIdAsync(int id)
    {
        logger.LogInformation($"Getting Resturant By Id : {id}");
        var resturant = await resturantRepository.GetByIdAsync(id);
        var resturantDto = mapper.Map<ResturantDto?>(resturant);
        return resturantDto;
    }
}