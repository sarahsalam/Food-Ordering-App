using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;

namespace FoodOrdering.Api.Services.Interfaces;

public interface IRestaurantBranchService
{
    Task<(bool Exists, List<BranchResponse> Data)> GetByRestaurantIdAsync(
        Guid restaurantId,
        BranchQueryRequest query);
    Task<BranchResponse?> GetByIdAsync(Guid id);
    Task<(bool Success, string? Error, BranchResponse? Data)> CreateAsync(
        Guid restaurantId,
        Guid ownerId,
        CreateBranchRequest request);
    Task<(bool Success, string? Error, BranchResponse? Data)> UpdateAsync(Guid id, UpdateBranchRequest request);
    Task<(bool Success, string? Error)> DeleteAsync(Guid id);
}
