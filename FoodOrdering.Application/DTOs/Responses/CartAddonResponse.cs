namespace FoodOrdering.Application.DTOs.Responses;

public record CartAddonResponse(Guid AddonId, string Name, decimal UnitPrice);
