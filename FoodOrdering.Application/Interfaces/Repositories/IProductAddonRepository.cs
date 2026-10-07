
namespace FoodOrdering.Application.Interfaces.Repositories;

public interface IProductAddonRepository
{
    Task<bool> ProductExistsAsync(Guid id);
    Task<List<ProductAddon>> GetByProductAsync(Guid id);
    Task<Addon?> GetAddonAsync(Guid id);
    Task<Guid?> GetProductRestaurantIdAsync(Guid productId);
    Task<bool> ExistsAsync(Guid productId, Guid addonId);
    Task<ProductAddon?> GetTrackedAsync(Guid productId, Guid addonId);
    Task AddAsync(ProductAddon productAddon);
    void Remove(ProductAddon productAddon);
    Task SaveChangesAsync();
}
