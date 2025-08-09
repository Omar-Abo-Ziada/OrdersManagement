namespace MyResturants.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}