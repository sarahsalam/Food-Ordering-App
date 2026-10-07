namespace FoodOrdering.Application.DTOs.Responses;

public record AddonResponse(
    Guid Id,
    string Name,
    decimal Price,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public Guid? RestaurantId { get; init; }
}
