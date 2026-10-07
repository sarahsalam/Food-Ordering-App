namespace FoodOrdering.Application.DTOs.Responses;

public record OrderItemResponse(
    Guid Id,
    Guid OrderId,
    Guid ProductId,
    Guid? ProductVariantId,
    string ProductName,
    string? VariantName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice);
