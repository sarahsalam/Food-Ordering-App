namespace FoodOrdering.Api.Models.Responses;

public record ProductResponse(
    Guid Id,
    Guid CategoryId,
    string Name,
    string? Description,
    decimal BasePrice,
    string? ImageUrl,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt);