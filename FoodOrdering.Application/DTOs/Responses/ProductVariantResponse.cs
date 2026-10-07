namespace FoodOrdering.Application.DTOs.Responses;

public record ProductVariantResponse(
    Guid Id,
    Guid ProductId,
    string Name,
    decimal Price,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
