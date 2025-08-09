using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Exceptions;
using MyResturants.Domain.Repositories;

namespace MyResturants.Application.Resturants.Commands.DeleteResturant;

public class DeleteResturantCommandHandler
    (ILogger<DeleteResturantCommandHandler> logger,
    IResturantRepository resturantRepository)
    : IRequestHandler<DeleteResturantCommand>
{
    public async Task Handle(DeleteResturantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting Resturant By Id : {request.Id}");

        var resturant = await resturantRepository.GetByIdAsync(request.Id);
        if (resturant is null)
            throw new CustomNotFoundException(nameof(Resturant), request.Id.ToString());

        await resturantRepository.Delete(resturant);
    }
}