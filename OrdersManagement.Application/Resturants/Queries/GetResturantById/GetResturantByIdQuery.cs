using MediatR;
using MyResturants.Application.Resturants.Dtos;
using System.Text.Json.Serialization;

namespace MyResturants.Application.Resturants.Queries.GetResturantById;

public class GetResturantByIdQuery(int id) : IRequest<ResturantDto>
{
    //[JsonIgnore] // This will prevent this prop from being serialized from body because I am getting it from route
    public int Id { get; } = id;
}