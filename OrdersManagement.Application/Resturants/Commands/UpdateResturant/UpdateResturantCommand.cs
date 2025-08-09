using MediatR;

namespace MyResturants.Application.Resturants.Commands.UpdateResturant;

public class UpdateResturantCommand(int id) : IRequest
{
    public int Id { get; set; } = id;

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;

    public bool HasDelivery { get; set; }

    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }

    //public void SetId(int id)
    //{
    //    Id = id;
    //}
}