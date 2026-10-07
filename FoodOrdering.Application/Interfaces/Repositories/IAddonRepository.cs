
namespace FoodOrdering.Application.Interfaces.Repositories;

public interface IAddonRepository
{
    Task<List<Addon>> GetAsync(string? search, bool? isActive);
    Task<Addon?> GetByIdAsync(Guid id);
    Task<Addon?> GetTrackedByIdAsync(Guid id);
    Task AddAsync(Addon addon);
    Task SaveChangesAsync();
}
