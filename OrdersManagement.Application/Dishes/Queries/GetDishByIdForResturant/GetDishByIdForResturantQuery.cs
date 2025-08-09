using MediatR;
using MyResturants.Application.Dishes.Dtos;

namespace MyResturants.Application.Dishes.Queries.GetDishByIdForResturant;

public class GetDishByIdForResturantQuery(int resturantId, int dishId) : IRequest<DishDto>
{
    public int ResturantId { get; } = resturantId;
    public int DishId { get; } = dishId;
}