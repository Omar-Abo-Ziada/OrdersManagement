using MediatR;
using OrdersManagement.Application.Helpers;
using MyResturants.Application.Resturants.Dtos;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Exceptions;
using MyResturants.Domain.Repositories;

namespace MyResturants.Application.Resturants.Queries.GetResturantById;

public class GetResturantByIdQueryHandler 
    (ILoggerHelper<GetResturantByIdQueryHandler> logger , IMapperHelper mapper , IResturantRepository resturantRepository)
    : IRequestHandler<GetResturantByIdQuery, ResturantDto>
{
    public async Task<ResturantDto> Handle(GetResturantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting Resturant By Id : {request.Id}");
        var resturant = await resturantRepository.GetByIdAsync(request.Id);
        var resturantDto = mapper.Map<ResturantDto?>(resturant)
            ?? throw new CustomNotFoundException(nameof(Resturant) , request.Id.ToString());
       
        return resturantDto;
    }
}