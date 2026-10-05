namespace FoodOrdering.Api.Models.Responses;

public record OrderItemAddonResponse(
    Guid Id,
    Guid OrderItemId,
    Guid AddonId,
    string AddonName,
    decimal UnitPrice,
    int Quantity);