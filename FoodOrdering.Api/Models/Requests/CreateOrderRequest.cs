using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record CreateOrderRequest(
    Guid UserId,
    Guid RestaurantId,
    Guid BranchId,
    string Status,
    decimal Subtotal,
    decimal Tax,
    decimal DeliveryFee,
    decimal Discount,
    decimal TotalAmount,
    string DeliveryAddress);