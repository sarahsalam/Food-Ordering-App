
namespace FoodOrdering.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<bool> CategoryExistsAsync(Guid id);
    Task<List<Product>> GetByCategoryAsync(Guid id, string? search, bool? isActive);
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product?> GetTrackedByIdAsync(Guid id);
    Task AddAsync(Product product);
    Task SaveChangesAsync();
}
