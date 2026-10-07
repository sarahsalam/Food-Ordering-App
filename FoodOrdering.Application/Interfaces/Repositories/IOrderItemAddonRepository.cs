
namespace FoodOrdering.Application.Interfaces.Repositories;

public interface IOrderItemAddonRepository
{
    Task<bool> OrderItemExistsAsync(Guid id);
    Task<List<OrderItemAddon>> GetByOrderItemAsync(Guid id);
    Task<OrderItemAddon?> GetByIdAsync(Guid id);
    Task<OrderItemAddon?> GetTrackedByIdAsync(Guid id);
    Task<Addon?> GetAddonAsync(Guid id);
    Task AddAsync(OrderItemAddon orderItemAddon);
    void Remove(OrderItemAddon orderItemAddon);
    Task SaveChangesAsync();
}
