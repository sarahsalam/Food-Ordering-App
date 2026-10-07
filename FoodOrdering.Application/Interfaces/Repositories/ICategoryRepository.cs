using FoodOrdering.Application.DTOs.Requests;

namespace FoodOrdering.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetByRestaurantIdAsync(Guid restaurantId, CategoryQueryRequest query);
    Task<Category?> GetByIdAsync(Guid id);
    Task<bool> RestaurantExistsAsync(Guid restaurantId);
    Task AddAsync(Category category);
    Task SaveChangesAsync();
}
