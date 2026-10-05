namespace FoodOrdering.Api.Models.Entities;

public class RestaurantStaff
{
    public Guid UserId { get; set; }
    public Guid RestaurantId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public Restaurant Restaurant { get; set; } = null!;
}
