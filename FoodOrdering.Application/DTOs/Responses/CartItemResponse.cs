namespace FoodOrdering.Application.DTOs.Responses;

public record CartItemResponse(
    Guid Id,
    Guid CartId,
    Guid ProductId,
    Guid? ProductVariantId,
    int Quantity,
    decimal UnitPrice,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public List<CartAddonResponse> Addons { get; init; } = [];
}
