using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyResturants.Application.Dishes.Dtos;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Exceptions;
using MyResturants.Domain.Repositories;

namespace MyResturants.Application.Dishes.Queries.GetDishByIdForResturant;

public class GetDishByIdForResturantQueryHandler 
    (ILogger<GetDishByIdForResturantQueryHandler> logger , IMapper mapper ,
    IResturantRepository resturantRepository)
    : IRequestHandler<GetDishByIdForResturantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForResturantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting Dish By Id : {request.DishId} for resturant with Id : {request.ResturantId}");

        var resturant = await resturantRepository.GetByIdAsync(request.ResturantId);
        if (resturant is null)
            throw new CustomNotFoundException(nameof(Resturant), request.ResturantId.ToString());

        var dish = resturant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
        if (dish is null)
            throw new CustomNotFoundException(nameof(Dish), request.DishId.ToString());

        var dishDto = mapper.Map<DishDto?>(dish);

        return dishDto!;
    }
}
