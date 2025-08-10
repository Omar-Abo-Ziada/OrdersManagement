//using MediatR;
//using MyResturants.Application.Users;
//using OrdersManagement.Application.Helpers;
//using OrdersManagement.Application.Common.Responses;
//using MyResturants.Domain.Entities;
//using MyResturants.Domain.Exceptions;
//using System.Net;

//namespace MyResturants.Application.Resturants.Commands.CreateResturant;

//public class CreateResturantCommandHandler(ILoggerHelper<CreateResturantCommandHandler> logger ,
//    IMapperHelper mapper , 
//    IUserContext userContext,
//    IResturantRepository resturantRepository): IRequestHandler<CreateResturantCommand, CustomResultDTO<CreateResturantResponse>>
//{
//    public async Task<CustomResultDTO<CreateResturantResponse>> Handle(CreateResturantCommand request, CancellationToken cancellationToken)
//    {
//        var user = userContext.GetCurrentUser();

//        logger.LogInformation("{user.Email} [{user.Id}] Is Creating New Resturant {@resturant}" 
//            , user!.Email , user.Id , request);

//        try
//        {
//            var resturant = mapper.Map<Resturant>(request);
//            resturant.OwnerId = user.Id;

//            int id = await resturantRepository.CreateAsync(resturant);
            
//            var response = new CreateResturantResponse
//            {
//                Id = id,
//                Name = resturant.Name,
//                Category = resturant.Category
//            };

//            logger.LogInformation("Restaurant created successfully with ID {RestaurantId} for user {UserEmail}", 
//                id, user.Email);
            
//            return CustomResultDTO<CreateResturantResponse>.Success(
//                message: "Restaurant created successfully",
//                data: response
//            );
//        }
//        catch (CustomNotFoundException ex)
//        {
//            logger.LogWarning(ex, "Restaurant creation failed: {Message}", ex.Message);
//            return CustomResultDTO<CreateResturantResponse>.Failure(
//                message: ex.Message,
//                statusCode: HttpStatusCode.NotFound
//            );
//        }
//        catch (Exception ex)
//        {
//            logger.LogError(ex, "Error creating restaurant for user {UserEmail}", user.Email);
//            return CustomResultDTO<CreateResturantResponse>.Failure(
//                message: "An error occurred while creating the restaurant",
//                statusCode: HttpStatusCode.InternalServerError
//            );
//        }
//    }
//}   