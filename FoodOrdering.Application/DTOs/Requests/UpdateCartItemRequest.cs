namespace FoodOrdering.Application.DTOs.Requests;

public record UpdateCartItemRequest(int Quantity)
{
    public List<Guid>? AddonIds { get; init; }
}
