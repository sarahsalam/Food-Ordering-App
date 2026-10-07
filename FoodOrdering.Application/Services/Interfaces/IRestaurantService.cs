using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IRestaurantService
{
    Task<List<RestaurantResponse>> GetAllAsync(Guid ownerId, RestaurantQueryRequest query);
    Task<RestaurantResponse?> GetByIdAsync(Guid ownerId, Guid id);
    Task<(bool Success, string? Error, RestaurantResponse? Data)> CreateAsync(
        Guid ownerId,
        CreateRestaurantRequest request);
    Task<(bool Success, string? Error, RestaurantResponse? Data)> UpdateAsync(Guid id, UpdateRestaurantRequest request);
    Task<(bool Success, string? Error)> DeleteAsync(Guid id);
}
