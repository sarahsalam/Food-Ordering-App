namespace FoodOrdering.Api.Models.Entities;

public class Restaurant
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Phone { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? LogoUrl { get; set; }

    public bool IsActive { get; set; }

    public RestaurantApprovalStatus ApprovalStatus { get; set; } = RestaurantApprovalStatus.Pending;
    public Guid? ReviewedByUserId { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public string? ReviewNote { get; set; }

    public Guid OwnerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ApplicationUser Owner { get; set; } = null!;

    public ICollection<RestaurantBranch> Branches { get; set; } = [];

    public ICollection<Category> Categories { get; set; } = [];

    public ICollection<Order> Orders { get; set; } = [];
}
