using FoodOrdering.Api.Models.Entities;

namespace FoodOrdering.Api.Repositories.Interfaces;

public interface IProductVariantRepository
{
    Task<bool> ProductExistsAsync(Guid id);
    Task<List<ProductVariant>> GetByProductAsync(Guid id, string? search, bool? isActive);
    Task<ProductVariant?> GetByIdAsync(Guid id);
    Task<ProductVariant?> GetTrackedByIdAsync(Guid id);
    Task AddAsync(ProductVariant productVariant);
    Task SaveChangesAsync();
}