
namespace FoodOrdering.Application.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task<bool> OrderExistsAsync(Guid id);
    Task<List<OrderItem>> GetByOrderAsync(Guid id);
    Task<OrderItem?> GetByIdAsync(Guid id);
    Task<OrderItem?> GetTrackedByIdAsync(Guid id);
    Task<(Product? Product, ProductVariant? Variant)> GetPricingAsync(Guid productId, Guid? productVariantId);
    Task AddAsync(OrderItem orderItem);
    Task SaveChangesAsync();
}
