using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record CreateCartItemRequest(Guid ProductId, Guid? ProductVariantId, int Quantity)
{
    public List<Guid>? AddonIds { get; init; }
}