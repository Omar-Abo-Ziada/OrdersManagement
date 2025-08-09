using MediatR;

namespace MyResturants.Application.Dishes.Commands.DeleteAllForResturant;

public class DeleteAllDishesForResturantCommand(int resturantId) : IRequest
{
    public int ResturantId { get; } = resturantId;
}