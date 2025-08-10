//using MediatR;
//using OrdersManagement.Application.Helpers;
//using OrdersManagement.Application.Common.Responses;
//using MyResturants.Domain.Exceptions;
//using OrdersManagement.Domain.Repositories;
//using System.Net;

//namespace MyResturants.Application.Dishes.Commands.CreateDish;

//public class CreateDishCommandHandler
//    (ILoggerHelper<CreateDishCommandHandler> logger, IMapperHelper mapper,
//    IGenericRepository<> dishRepository)
//    : IRequestHandler<CreateDishCommand, CustomResultDTO<CreateDishResponse>>
//{
//    public async Task<CustomResultDTO<CreateDishResponse>> Handle(CreateDishCommand request, CancellationToken cancellationToken)
//    {
//        logger.LogInformation("Creating Dish : {@request}", request);
        
//        try
//        {
//            var resturant = await resturantRepository.GetByIdAsync(request.ResturantId);
//            if (resturant is null)
//            {
//                logger.LogWarning("Restaurant not found with ID: {RestaurantId}", request.ResturantId);
//                return CustomResultDTO<CreateDishResponse>.Failure(
//                    message: "Restaurant not found",
//                    statusCode: HttpStatusCode.NotFound
//                );
//            }

//            // I can use resturant repo to create a dish or for better practice I can use a dish repo to create a dish
//            var dish = mapper.Map<Dish>(request);

//            var dishId = await dishRepository.CreateAsync(dish);

//            var response = new CreateDishResponse
//            {
//                Id = dishId,
//                Name = dish.Name,
//                Price = dish.Price
//            };

//            logger.LogInformation("Dish created successfully with ID: {DishId}", dishId);
            
//            return CustomResultDTO<CreateDishResponse>.Success(
//                message: "Dish created successfully",
//                data: response,
//                statusCode: HttpStatusCode.Created
//            );
//        }
//        catch (CustomNotFoundException ex)
//        {
//            logger.LogWarning(ex, "Dish creation failed: {Message}", ex.Message);
//            return CustomResultDTO<CreateDishResponse>.Failure(
//                message: ex.Message,
//                statusCode: HttpStatusCode.NotFound
//            );
//        }
//        catch (Exception ex)
//        {
//            logger.LogError(ex, "Error creating dish for restaurant {RestaurantId}", request.ResturantId);
//            return CustomResultDTO<CreateDishResponse>.Failure(
//                message: "An error occurred while creating the dish",
//                statusCode: HttpStatusCode.InternalServerError
//            );
//        }
//    }
//}