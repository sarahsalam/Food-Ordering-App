
namespace FoodOrdering.Application.Interfaces.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetByIdAsync(Guid id);
    Task<Cart?> GetTrackedByIdAsync(Guid id);
    Task<bool> UserExistsAsync(Guid id);
    Task<bool> ExistsForUserAsync(Guid id);
    Task AddAsync(Cart cart);
    void Remove(Cart cart);
    Task SaveChangesAsync();
    Task<Cart?> GetForUserAsync(Guid userId);
}
