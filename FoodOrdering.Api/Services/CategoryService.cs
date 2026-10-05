using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;

namespace FoodOrdering.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool Exists, List<CategoryResponse> Data)> GetByRestaurantIdAsync(
        Guid restaurantId,
        CategoryQueryRequest query)
    {
        if (!await _repository.RestaurantExistsAsync(restaurantId))
        {
            return (false, []);
        }

        var categories = await _repository.GetByRestaurantIdAsync(restaurantId, query);
        return (true, categories.Select(Map).ToList());
    }

    public async Task<CategoryResponse?> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        return category == null ? null : Map(category);
    }

    public async Task<(bool Success, string? Error, CategoryResponse? Data)> CreateAsync(
        Guid restaurantId,
        CreateCategoryRequest request)
    {
        if (!await _repository.RestaurantExistsAsync(restaurantId))
        {
            return (false, "Restaurant was not found.", null);
        }

        var category = new Category
        {
            Id = Guid.NewGuid(),
            RestaurantId = restaurantId,
            Name = request.Name,
            Description = request.Description,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();
        return (true, null, Map(category));
    }

    public async Task<(bool Success, string? Error, CategoryResponse? Data)> UpdateAsync(
        Guid id,
        UpdateCategoryRequest request)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
        {
            return (false, "Category was not found.", null);
        }

        category.Name = request.Name;
        category.Description = request.Description;
        category.IsActive = request.IsActive;
        category.UpdatedAt = DateTime.UtcNow;

        await _repository.SaveChangesAsync();
        return (true, null, Map(category));
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
        {
            return (false, "Category was not found.");
        }

        category.IsActive = false;
        category.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null);
    }

    public static CategoryResponse Map(Category category) => new()
    {
        Id = category.Id,
        RestaurantId = category.RestaurantId,
        Name = category.Name,
        Description = category.Description,
        IsActive = category.IsActive,
        CreatedAt = category.CreatedAt,
        UpdatedAt = category.UpdatedAt
    };
}