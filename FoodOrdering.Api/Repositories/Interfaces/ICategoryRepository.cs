using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;

namespace FoodOrdering.Api.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetByRestaurantIdAsync(Guid restaurantId, CategoryQueryRequest query);
    Task<Category?> GetByIdAsync(Guid id);
    Task<bool> RestaurantExistsAsync(Guid restaurantId);
    Task AddAsync(Category category);
    Task SaveChangesAsync();
}