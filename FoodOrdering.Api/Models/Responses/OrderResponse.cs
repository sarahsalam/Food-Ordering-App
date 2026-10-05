namespace FoodOrdering.Api.Models.Responses;

public record OrderResponse(
    Guid Id,
    Guid UserId,
    Guid RestaurantId,
    Guid BranchId,
    string Status,
    decimal Subtotal,
    decimal Tax,
    decimal DeliveryFee,
    decimal Discount,
    decimal TotalAmount,
    string DeliveryAddress,
    DateTime CreatedAt,
    DateTime? UpdatedAt);