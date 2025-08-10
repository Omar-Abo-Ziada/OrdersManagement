//using MediatR;
//using OrdersManagement.Application.Common.Responses;

//namespace MyResturants.Application.Resturants.Commands.CreateResturant;

//public class CreateResturantCommand : IRequest<CustomResultDTO<CreateResturantResponse>>
//{
//    public string Name { get; set; } = string.Empty;
//    public string Description { get; set; } = string.Empty;
//    public string Category { get; set; } = string.Empty;

//    public bool HasDelivery { get; set; }

//    public string? ContactEmail { get; set; }
//    public string? ContactNumber { get; set; }

//    public string? City { get; set; }
//    public string? Street { get; set; }
//    public string? PostalCode { get; set; }
//}

//public class CreateResturantResponse
//{
//    public int Id { get; set; }
//    public string Name { get; set; } = string.Empty;
//    public string Category { get; set; } = string.Empty;
//}