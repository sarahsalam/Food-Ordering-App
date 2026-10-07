namespace FoodOrdering.Application.DTOs.Responses;

public record ProductAddonResponse(Guid ProductId, Guid AddonId, AddonResponse Addon);
