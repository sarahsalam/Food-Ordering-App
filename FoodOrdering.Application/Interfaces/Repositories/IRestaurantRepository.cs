using FoodOrdering.Application.DTOs.Requests;

namespace FoodOrdering.Application.Interfaces.Repositories;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllAsync(Guid ownerId, RestaurantQueryRequest query);
    Task<Restaurant?> GetByIdAsync(Guid ownerId, Guid id);
    Task<Restaurant?> GetByIdAsync(Guid id);
    Task<bool> OwnerExistsAsync(Guid ownerId);
    Task AddAsync(Restaurant restaurant);
    Task SaveChangesAsync();
    Task<List<Restaurant>> GetAvailableAsync();
}
