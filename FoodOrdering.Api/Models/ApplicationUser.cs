using FoodOrdering.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodOrdering.Api.Models.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

    public ICollection<Restaurant> Restaurants { get; set; } = [];

    public Cart? Cart { get; set; }

    public ICollection<Order> Orders { get; set; } = [];
}