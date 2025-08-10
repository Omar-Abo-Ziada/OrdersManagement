//using MediatR;
//using OrdersManagement.Application.Common.Responses;
//using System.Text.Json.Serialization;

//namespace MyResturants.Application.Dishes.Commands.CreateDish;

//public class CreateDishCommand : IRequest<CustomResultDTO<CreateDishResponse>>
//{
//    public string Name { get; set; } = default!;
//    public string Description { get; set; } = default!;
//    public decimal Price { get; set; }

//    public int? KiloCalories { get; set; }

//    [JsonIgnore] // This will prevent this prop from being serialized from body because I am getting it from route
//    public int ResturantId { get; set; }
//}

//public class CreateDishResponse
//{
//    public int Id { get; set; }
//    public string Name { get; set; } = default!;
//    public decimal Price { get; set; }
//}