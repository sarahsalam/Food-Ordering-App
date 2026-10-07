using FoodOrdering.Application.DTOs.Requests;

namespace FoodOrdering.Application.Interfaces.Repositories;

public interface IRestaurantBranchRepository
{
    Task<List<RestaurantBranch>> GetByRestaurantIdAsync(
        Guid restaurantId,
        BranchQueryRequest query);
    Task<RestaurantBranch?> GetByIdAsync(Guid id);
    Task<bool> RestaurantExistsAsync(Guid restaurantId);
    Task<bool> RestaurantOwnedByUserAsync(Guid restaurantId, Guid ownerId);
    Task AddAsync(RestaurantBranch branch);
    Task SaveChangesAsync();
}
