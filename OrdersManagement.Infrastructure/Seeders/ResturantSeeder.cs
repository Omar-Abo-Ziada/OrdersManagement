//using Microsoft.AspNetCore.Identity;
//using MyResturants.Domain.Constants;
//using OrdersManagement.Infrastructure.Presistance;

//namespace MyResturants.Infrastructure.Seeders;

//internal class ResturantSeeder(ApplicationDbContext context) : ISeeder
//{
//    public async Task Seed() // => Must be public so it can be called from the interface
//    {
//        if (await context.Database.CanConnectAsync())
//        {
//            //if (!context.Resturants.Any())
//            //{
//            //    var resturants = GetResturants();
//            //    context.Resturants.AddRange(resturants);
//            //    await context.SaveChangesAsync();
//            //}

//            if (!context.Roles.Any())
//            {
//                var roles = GetRoles();
//                context.Roles.AddRange(roles);
//                await context.SaveChangesAsync();
//            }
//        }
//    }

//    private IEnumerable<IdentityRole<int>> GetRoles()
//    {
//        List<IdentityRole<int>> roles =
//        [
//            new (UserRoles.Admin){NormalizedName = UserRoles.Admin.ToUpper() },
//            new (UserRoles.Customer){NormalizedName = UserRoles.Customer.ToUpper() },
//            new (UserRoles.Seller){NormalizedName = UserRoles.Seller.ToUpper() },
//            new (UserRoles.Rider){NormalizedName = UserRoles.Rider.ToUpper() },
//        ];

//        return roles;
//    }

//    //private IEnumerable<Resturant> GetResturants()
//    //{
//    //    List<Resturant> resturants = [
//    //        new()
//    //        {
//    //            //Owner = owner,
//    //            Name = "KFC",
//    //            Category = "Fast Food",
//    //            Description =
//    //                "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
//    //            ContactEmail = "contact@kfc.com",
//    //            HasDelivery = true,
//    //            Dishes =
//    //            [
//    //                new ()
//    //                {
//    //                    Name = "Nashville Hot Chicken",
//    //                    Description = "Nashville Hot Chicken (10 pcs.)",
//    //                    Price = 10.30M,
//    //                },

//    //                new ()
//    //                {
//    //                    Name = "Chicken Nuggets",
//    //                    Description = "Chicken Nuggets (5 pcs.)",
//    //                    Price = 5.30M,
//    //                },
//    //            ],
//    //            Address = new ()
//    //            {
//    //                City = "London",
//    //                Street = "Cork St 5",
//    //                PostalCode = "WC2N 5DU"
//    //            },

//    //        },
//    //        new ()
//    //        {
//    //            //Owner = owner,
//    //            Name = "McDonald",
//    //            Category = "Fast Food",
//    //            Description =
//    //                "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
//    //            ContactEmail = "contact@mcdonald.com",
//    //            HasDelivery = true,
//    //            Address = new Address()
//    //            {
//    //                City = "London",
//    //                Street = "Boots 193",
//    //                PostalCode = "W1F 8SR"
//    //            }
//    //        }
//    //    ];

//    //    return resturants;
//    //}
//}