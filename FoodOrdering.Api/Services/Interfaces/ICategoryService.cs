using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;

namespace FoodOrdering.Api.Services.Interfaces;

public interface ICategoryService
{
    Task<(bool Exists, List<CategoryResponse> Data)> GetByRestaurantIdAsync(
        Guid restaurantId,
        CategoryQueryRequest query);

    Task<CategoryResponse?> GetByIdAsync(Guid id);

    Task<(bool Success, string? Error, CategoryResponse? Data)> CreateAsync(
        Guid restaurantId,
        CreateCategoryRequest request);

    Task<(bool Success, string? Error, CategoryResponse? Data)> UpdateAsync(
        Guid id,
        UpdateCategoryRequest request);

    Task<(bool Success, string? Error)> DeleteAsync(Guid id);
}