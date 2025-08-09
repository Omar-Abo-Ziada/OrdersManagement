using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyResturants.Application.Resturants.Commands.CreateResturant;
using MyResturants.Application.Resturants.Commands.DeleteResturant;
using MyResturants.Application.Resturants.Commands.UpdateResturant;
using MyResturants.Application.Resturants.Queries.GetAllResturants;
using MyResturants.Application.Resturants.Queries.GetResturantById;
using MyResturants.Domain.Entities;
using MyResturants.Infrastructure.Authorization;

namespace MyResturants.Presentaion.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResturantsController(IMediator mediator)
    : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = PolicyNames.AtLeast20)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Resturant>>> GetAll()
    {
        var resturants = await mediator.Send(new GetAllResturantsQuery());
        return Ok(resturants);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = PolicyNames.HasNationality)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Resturant>> GetById([FromRoute] int id)
    {
        var resturant = await mediator.Send(new GetResturantByIdQuery(id));
        return Ok(resturant);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Resturant>> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeleteResturantCommand(id));
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> Create(CreateResturantCommand createResturantCommand)
    {
        int id = await mediator.Send(createResturantCommand);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> Update([FromRoute] int id, UpdateResturantCommand updateResturantCommand)
    {
        updateResturantCommand.Id = id;
        await mediator.Send(updateResturantCommand);
        return CreatedAtAction(nameof(GetById), new { updateResturantCommand.Id }, null);
    }
}