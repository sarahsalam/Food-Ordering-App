namespace FoodOrdering.Api.Models.Responses;

public record CartAddonResponse(Guid AddonId, string Name, decimal UnitPrice);