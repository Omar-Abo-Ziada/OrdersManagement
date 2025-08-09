using MediatR;
using MyResturants.Application.Users;
using OrdersManagement.Application.Helpers;

namespace MyResturants.Application.Resturants.Commands.CreateResturant;

public class CreateResturantCommandHandler(ILoggerHelper<CreateResturantCommandHandler> logger ,
    IMapperHelper mapper , 
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