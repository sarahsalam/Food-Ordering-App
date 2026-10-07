namespace FoodOrdering.Application.DTOs.Requests;

public record UpdateOrderRequest(
    string Status,
    decimal Subtotal,
    decimal Tax,
    decimal DeliveryFee,
    decimal Discount,
    decimal TotalAmount,
    string DeliveryAddress);
