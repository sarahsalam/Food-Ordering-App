namespace FoodOrdering.Api.Models.Responses;

public record ProductAddonResponse(Guid ProductId, Guid AddonId, AddonResponse Addon);