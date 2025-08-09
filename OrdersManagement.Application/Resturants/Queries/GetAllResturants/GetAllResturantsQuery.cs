using MediatR;
using MyResturants.Application.Resturants.Dtos;

namespace MyResturants.Application.Resturants.Queries.GetAllResturants;

public class GetAllResturantsQuery : IRequest<IEnumerable<ResturantDto>>
{
}