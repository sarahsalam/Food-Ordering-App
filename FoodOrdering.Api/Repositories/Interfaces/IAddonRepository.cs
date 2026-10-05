using FoodOrdering.Api.Models.Entities;

namespace FoodOrdering.Api.Repositories.Interfaces;

public interface IAddonRepository
{
    Task<List<Addon>> GetAsync(string? search, bool? isActive);
    Task<Addon?> GetByIdAsync(Guid id);
    Task<Addon?> GetTrackedByIdAsync(Guid id);
    Task AddAsync(Addon addon);
    Task SaveChangesAsync();
}