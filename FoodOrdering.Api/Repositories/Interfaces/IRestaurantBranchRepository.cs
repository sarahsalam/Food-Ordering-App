using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Entities;

namespace FoodOrdering.Api.Repositories.Interfaces;

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
