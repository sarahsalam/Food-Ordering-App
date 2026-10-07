using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Application.DTOs.Requests;

public record CheckoutRequest(
    Guid BranchId,
    [Required] string DeliveryAddress)
{
    public string? CouponCode { get; init; }
}
