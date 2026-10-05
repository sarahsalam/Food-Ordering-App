using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record CheckoutRequest(
    Guid BranchId,
    [Required] string DeliveryAddress)
{
    public string? CouponCode { get; init; }
}
