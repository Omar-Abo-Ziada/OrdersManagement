using Microsoft.AspNetCore.Identity;
using OrdersManagement.Domain.Entities.Order_Module;

namespace OrdersManagement.Domain.Entities.User_Module;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }

    // Navigation
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}