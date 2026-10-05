using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Entities;

namespace FoodOrdering.Api.Repositories.Interfaces;

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