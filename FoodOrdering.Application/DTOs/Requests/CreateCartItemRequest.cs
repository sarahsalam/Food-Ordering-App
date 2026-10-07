namespace FoodOrdering.Application.DTOs.Requests;

public record CreateCartItemRequest(Guid ProductId, Guid? ProductVariantId, int Quantity)
{
    public List<Guid>? AddonIds { get; init; }
}
