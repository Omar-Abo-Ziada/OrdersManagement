using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyResturants.Application.Resturants.Dtos;
using MyResturants.Domain.Repositories;

namespace MyResturants.Application.Resturants.Queries.GetAllResturants;

public class GetAllResturantsQueryHandler
    (ILogger<GetAllResturantsQueryHandler> logger, IMapper mapper, IResturantRepository resturantRepository)
    : IRequestHandler<GetAllResturantsQuery, IEnumerable<ResturantDto>>
{
    public async Task<IEnumerable<ResturantDto>> Handle(GetAllResturantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Resturants");
        var resturants = await resturantRepository.GetAllAsync();
        var resturantDtos = mapper.Map<IEnumerable<ResturantDto>>(resturants);
        return resturantDtos;
    }
}