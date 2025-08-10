//using Mapster;
//using MyResturants.Application.Dishes.Commands.CreateDish;

//namespace MyResturants.Application.Dishes.Dtos
//{
//    public class DishesMappingConfig : IRegister
//    {
//        public void Register(TypeAdapterConfig config)
//        {
//            // Command -> Entity
//            config.NewConfig<CreateDishCommand, Dish>();

//            // Entity -> DTO
//            config.NewConfig<Dish, DishDto>()
//                .Map(dest => dest.KiloCalories, src => src.KiloCalories ?? 0);
//        }
//    }
//}