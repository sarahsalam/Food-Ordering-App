using FoodOrdering.Api.Models.Entities;

namespace FoodOrdering.Api.Repositories.Interfaces;

public interface IResourceAccessRepository
{
    Task<bool> OwnsCartAsync(Guid cartId, Guid userId);
    Task<bool> OwnsCartItemAsync(Guid itemId, Guid userId);
    Task<bool> OwnsOrderAsync(Guid orderId, Guid userId);
    Task<bool> OwnsOrderItemAsync(Guid itemId, Guid userId);
    Task<bool> OwnsOrderItemAddonAsync(Guid addonId, Guid userId);
    Task<Guid?> GetAddonRestaurantIdAsync(Guid id);
    Task<Guid?> GetBranchRestaurantIdAsync(Guid id);
    Task<Guid?> GetCategoryRestaurantIdAsync(Guid id);
    Task<Guid?> GetProductRestaurantIdAsync(Guid id);
    Task<Guid?> GetVariantRestaurantIdAsync(Guid id);
    Task<Restaurant?> GetRestaurantAsync(Guid? id);
}
