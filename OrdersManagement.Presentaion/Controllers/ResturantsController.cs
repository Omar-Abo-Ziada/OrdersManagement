//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using MyResturants.Application.Resturants.Commands.CreateResturant;
//using MyResturants.Application.Resturants.Commands.DeleteResturant;
//using MyResturants.Application.Resturants.Commands.UpdateResturant;
//using MyResturants.Application.Resturants.Queries.GetAllResturants;
//using MyResturants.Application.Resturants.Queries.GetResturantById;
//using MyResturants.Infrastructure.Authorization;
//using OrdersManagement.Application.Common.Responses;
//using System.Net;

//namespace MyResturants.Presentaion.Controllers;

//[Route("api/[controller]")]
//[Produces("application/json")]
//[ApiController]
//public class ResturantsController : BaseController
//{
//    private readonly IMediator _mediator;

//    public ResturantsController(IMediator mediator)
//    {
//        _mediator = mediator;
//    }

//    /// <summary>
//    /// Get all restaurants (requires user to be at least 20 years old)
//    /// </summary>
//    /// <returns>List of all restaurants</returns>
//    [HttpGet]
//    [Authorize(Policy = PolicyNames.AtLeast20)]
//    [ProducesResponseType(typeof(IEnumerable<Resturant>), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(StatusCodes.Status403Forbidden)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<ActionResult<IEnumerable<Resturant>>> GetAll()
//    {
//        var resturants = await _mediator.Send(new GetAllResturantsQuery());
//        return Ok(resturants);
//    }

//    /// <summary>
//    /// Get a specific restaurant by ID (requires user to have nationality)
//    /// </summary>
//    /// <param name="id">Restaurant ID</param>
//    /// <returns>Restaurant details</returns>
//    [HttpGet("{id:int}")]
//    [Authorize(Policy = PolicyNames.HasNationality)]
//    [ProducesResponseType(typeof(Resturant), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(StatusCodes.Status403Forbidden)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<ActionResult<Resturant>> GetById([FromRoute] int id)
//    {
//        var resturant = await _mediator.Send(new GetResturantByIdQuery(id));
//        return Ok(resturant);
//    }

//    /// <summary>
//    /// Delete a restaurant by ID
//    /// </summary>
//    /// <param name="id">Restaurant ID to delete</param>
//    /// <returns>No content on successful deletion</returns>
//    [HttpDelete("{id:int}")]
//    [ProducesResponseType(StatusCodes.Status204NoContent)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<ActionResult<Resturant>> Delete([FromRoute] int id)
//    {
//        await _mediator.Send(new DeleteResturantCommand(id));
//        return NoContent();
//    }

//    /// <summary>
//    /// Create a new restaurant
//    /// </summary>
//    /// <param name="createResturantCommand">Restaurant creation details</param>
//    /// <returns>Created restaurant information</returns>
//    [HttpPost]
//    [ProducesResponseType(typeof(CustomResultDTO<CreateResturantResponse>), StatusCodes.Status201Created)]
//    [ProducesResponseType(typeof(CustomResultDTO<CreateResturantResponse>), StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(typeof(CustomResultDTO<CreateResturantResponse>), StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(typeof(CustomResultDTO<CreateResturantResponse>), StatusCodes.Status404NotFound)]
//    [ProducesResponseType(typeof(CustomResultDTO<CreateResturantResponse>), StatusCodes.Status500InternalServerError)]
//    public async Task<IActionResult> Create(CreateResturantCommand createResturantCommand)
//    {
//        var result = await _mediator.Send(createResturantCommand);
        
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
//    /// Update an existing restaurant
//    /// </summary>
//    /// <param name="id">Restaurant ID to update</param>
//    /// <param name="updateResturantCommand">Updated restaurant details</param>
//    /// <returns>Updated restaurant information</returns>
//    [HttpPut("{id:int}")]
//    [ProducesResponseType(StatusCodes.Status201Created)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//    public async Task<ActionResult<int>> Update([FromRoute] int id, UpdateResturantCommand updateResturantCommand)
//    {
//        updateResturantCommand.Id = id;
//        await _mediator.Send(updateResturantCommand);
//        return CreatedAtAction(nameof(GetById), new { updateResturantCommand.Id }, null);
//    }
//}