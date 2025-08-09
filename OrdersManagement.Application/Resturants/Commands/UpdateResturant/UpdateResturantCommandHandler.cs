using MediatR;
using OrdersManagement.Application.Helpers;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Exceptions;
using MyResturants.Domain.Repositories;

namespace MyResturants.Application.Resturants.Commands.UpdateResturant;

public class UpdateResturantCommandHandler
    (ILoggerHelper<UpdateResturantCommandHandler> logger,
    IMapperHelper mapper,
    IResturantRepository resturantRepository) : IRequestHandler<UpdateResturantCommand>
{
    public async Task Handle(UpdateResturantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating Resturant With Id : {request.Id}");

        var resturant = await resturantRepository.GetByIdAsync(request.Id);
        if (resturant is null)
            throw new CustomNotFoundException(nameof(Resturant), request.Id.ToString());

        mapper.Map(request, resturant);

        await resturantRepository.UpdateAsync(resturant);
    }
}