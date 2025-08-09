using MediatR;
using OrdersManagement.Application.Helpers;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Exceptions;
using MyResturants.Domain.Repositories;

namespace MyResturants.Application.Dishes.Commands.DeleteAllForResturant;

public class DeleteAllDishesForResturantCommandHandler
    (ILoggerHelper<DeleteAllDishesForResturantCommandHandler> logger,
    IResturantRepository resturantRepository ,
    IDishRepository dishRepository): IRequestHandler<DeleteAllDishesForResturantCommand>
{
    public async Task Handle(DeleteAllDishesForResturantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting All Dishes For Resturant By Id : {request.ResturantId}");

        var resturant =  await resturantRepository.GetByIdAsync(request.ResturantId);
        if (resturant is null)
            throw new CustomNotFoundException(nameof(Resturant), request.ResturantId.ToString());

        await dishRepository.RemoveAllForResturantAsync(request.ResturantId);
    }
}