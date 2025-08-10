//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using MyResturants.Application.Dishes.Commands.CreateDish;
//using MyResturants.Application.Dishes.Commands.DeleteAllForResturant;
//using MyResturants.Application.Dishes.Dtos;
//using MyResturants.Application.Dishes.Queries.GetAllForResturant;
//using MyResturants.Application.Dishes.Queries.GetDishByIdForResturant;
//using OrdersManagement.Application.Common.Responses;
//using System.Net;

//namespace MyResturants.Presentaion.Controllers;

//[Route("api/resturants/{resturantId}/[controller]")]
//[Produces("application/json")]
//[ApiController]
//public class DishesController : BaseController
//{
//    private readonly IMediator _mediator;

//    public DishesController(IMediator mediator)
//    {
//        _mediator = mediator;
//    }

//    /// <summary>
//    /// Create a new dish for a specific restaurant
//    /// </summary>
//    /// <param name="resturantId">Restaurant ID</param>
//    /// <param name="command">Dish creation details</param>
//    /// <returns>Created dish information</returns>
//    [HttpPost]
//    [ProducesResponseType(typeof(CustomResultDTO<CreateDishResponse>), StatusCodes.Status201Created)]
//    [ProducesResponseType(typeof(CustomResultDTO<CreateDishResponse>), StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(typeof(CustomResultDTO<CreateDishResponse>), StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(typeof(CustomResultDTO<CreateDishResponse>), StatusCodes.Status404NotFound)]
//    [ProducesResponseType(typeof(CustomResultDTO<CreateDishResponse>), StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> Create([FromRoute] int resturantId, CreateDishCommand command)
//    {
//        command.ResturantId = resturantId;
//        var result = await _mediator.Send(command);
        
//        return result.StatusCode switch
//        {
//            HttpStatusCode.Created => Success(result.Data!, result.Message, result.StatusCode),
//            HttpStatusCode.BadRequest => Failure(result.Message, result.Data, result.StatusCode, result.Errors),
//            HttpStatusCode.NotFound => NotFound(result.Message, result.Data),
//            HttpStatusCode.InternalServerError => InternalServerError(result.Message, result.Data),
//            _ => Failure(result.Message, result.Data, result.StatusCode, result.Errors)
//        };
//    }

//    /// <summary>
//    /// Get all dishes for a specific restaurant
//    /// </summary>
//    /// <param name="resturantId">Restaurant ID</param>
//    /// <returns>List of dishes for the restaurant</returns>
//    [HttpGet]
//    [ProducesResponseType(typeof(IEnumerable<DishDto>), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForResturant([FromRoute] int resturantId)
//    {
//        var dishes = await _mediator.Send(new GetAllForResturantQuery(resturantId));
//        return Ok(dishes);
//    }

//    /// <summary>
//    /// Get a specific dish by ID for a restaurant
//    /// </summary>
//    /// <param name="resturantId">Restaurant ID</param>
//    /// <param name="dishId">Dish ID</param>
//    /// <returns>Dish details</returns>
//    [HttpGet("{dishId:int}")]
//    [ProducesResponseType(typeof(DishDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<ActionResult<IEnumerable<DishDto>>> GetByIdForResturant([FromRoute] int resturantId, [FromRoute] int dishId)
//    {
//        var dishes = await _mediator.Send(new GetDishByIdForResturantQuery(resturantId, dishId));
//        return Ok(dishes);
//    }

//    /// <summary>
//    /// Delete all dishes for a specific restaurant
//    /// </summary>
//    /// <param name="resturantId">Restaurant ID</param>
//    /// <returns>No content on successful deletion</returns>
//    [HttpDelete]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> DeleteAllForResturant([FromRoute] int resturantId)
//    {
//        await _mediator.Send(new DeleteAllDishesForResturantCommand(resturantId));
//        return NoContent();
//    }
//}