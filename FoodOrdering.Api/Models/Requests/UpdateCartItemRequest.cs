using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record UpdateCartItemRequest(int Quantity)
{
    public List<Guid>? AddonIds { get; init; }
}
