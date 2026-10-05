using FoodOrdering.Api.Models.Entities;

namespace FoodOrdering.Api.Repositories.Interfaces;

public interface ICartItemRepository
{
    Task<bool> CartExistsAsync(Guid id);
    Task<List<CartItem>> GetByCartAsync(Guid id);
    Task<CartItem?> GetByIdAsync(Guid id);
    Task<CartItem?> GetTrackedByIdAsync(Guid id);
    Task<(Product? Product, ProductVariant? Variant)> GetPricingAsync(Guid productId, Guid? productVariantId);
    Task<List<Addon>> GetAvailableAddonsAsync(Guid productId, List<Guid> addonIds);
    Task AddAsync(CartItem cartItem);
    void Remove(CartItem cartItem);
    Task SaveChangesAsync();
}