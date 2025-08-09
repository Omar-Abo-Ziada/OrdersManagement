using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyResturants.Domain.Exceptions;
using OrdersManagement.Domain.Repositories;

namespace MyResturants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler
    (ILogger<CreateDishCommandHandler> logger, IMapper mapper,
    IResturantRepository resturantRepository, IGenericRepository<> dishRepository)
    : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating Dish : {@request}", request);
        var resturant = await resturantRepository.GetByIdAsync(request.ResturantId);
        if (resturant is null)
            throw new CustomNotFoundException(nameof(Resturant), request.ResturantId.ToString());

        // I can use resturant repo to create a dish or for better practice I can use a dish repo to create a dish
        var dish = mapper.Map<Dish>(request);

        var dishId = await dishRepository.CreateAsync(dish);

        return dishId;
    }
}