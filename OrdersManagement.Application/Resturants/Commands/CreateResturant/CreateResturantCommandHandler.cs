using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyResturants.Application.Users;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Repositories;

namespace MyResturants.Application.Resturants.Commands.CreateResturant;

public class CreateResturantCommandHandler(ILogger<CreateResturantCommandHandler> logger ,
    IMapper mapper , 
    IUserContext userContext,
    IResturantRepository resturantRepository): IRequestHandler<CreateResturantCommand, int>
{
    public async Task<int> Handle(CreateResturantCommand request, CancellationToken cancellationToken)
    {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("{user.Email} [{user.Id}] Is Creating New Resturant {@resturant}" 
                , user!.Email , user.Id , request);

            var resturant = mapper.Map<Resturant>(request);
            resturant.OwnerId = user.Id;

            int id = await resturantRepository.CreateAsync(resturant);
            return id;
    }
}   