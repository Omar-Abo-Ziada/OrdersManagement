using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Exceptions;

namespace MyResturants.Application.Users.Commands.UpdateUserDetails;

internal class UpdateUserDetailsCommandHandler 
    (ILogger<UpdateUserDetailsCommandHandler> logger ,
    IUserContext userContext , IUserStore<User> userStore)
    : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Updating User : {@UserId} , with : {@request}" , user!.Id  ,request);

        var dbUser = await userStore.FindByIdAsync(user.Id , cancellationToken);
        if(dbUser is null)
            throw new CustomNotFoundException(nameof(User), user.Id);

        dbUser.DateOfBirth = request.DateOfBirth;
        dbUser.Nationality = request.Nationality;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
