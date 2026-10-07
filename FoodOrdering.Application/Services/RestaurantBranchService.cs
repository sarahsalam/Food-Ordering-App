using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services;

public class RestaurantBranchService : IRestaurantBranchService
{
    private readonly IRestaurantBranchRepository _repository;

    public RestaurantBranchService(IRestaurantBranchRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool Exists, List<BranchResponse> Data)> GetByRestaurantIdAsync(
        Guid restaurantId,
        BranchQueryRequest query)
    {
        if (!await _repository.RestaurantExistsAsync(restaurantId))
        {
            return (false, []);
        }

        var branches = await _repository.GetByRestaurantIdAsync(restaurantId, query);
        return (true, branches.Select(Map).ToList());
    }

    public async Task<BranchResponse?> GetByIdAsync(Guid id)
    {
        var branch = await _repository.GetByIdAsync(id);
        return branch == null ? null : Map(branch);
    }

    public async Task<(bool Success, string? Error, BranchResponse? Data)> CreateAsync(
        Guid restaurantId,
        Guid ownerId,
        CreateBranchRequest request)
    {
        if (!await _repository.RestaurantExistsAsync(restaurantId))
        {
            return (false, "Restaurant was not found.", null);
        }

        if (!await _repository.RestaurantOwnedByUserAsync(restaurantId, ownerId))
        {
            return (false, "You do not own this restaurant.", null);
        }

        var branch = new RestaurantBranch
        {
            Id = Guid.NewGuid(),
            RestaurantId = restaurantId,
            Name = request.Name,
            Address = request.Address,
            City = request.City,
            Phone = request.Phone,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(branch);
        await _repository.SaveChangesAsync();
        return (true, null, Map(branch));
    }

    public async Task<(bool Success, string? Error, BranchResponse? Data)> UpdateAsync(
        Guid id,
        UpdateBranchRequest request)
    {
        var branch = await _repository.GetByIdAsync(id);
        if (branch == null)
        {
            return (false, "Branch was not found.", null);
        }

        branch.Name = request.Name;
        branch.Address = request.Address;
        branch.City = request.City;
        branch.Phone = request.Phone;
        branch.Latitude = request.Latitude;
        branch.Longitude = request.Longitude;
        branch.IsActive = request.IsActive;
        branch.UpdatedAt = DateTime.UtcNow;

        await _repository.SaveChangesAsync();
        return (true, null, Map(branch));
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(Guid id)
    {
        var branch = await _repository.GetByIdAsync(id);
        if (branch == null)
        {
            return (false, "Branch was not found.");
        }

        branch.IsActive = false;
        branch.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null);
    }

    public static BranchResponse Map(RestaurantBranch branch) => new()
    {
        Id = branch.Id,
        RestaurantId = branch.RestaurantId,
        Name = branch.Name,
        Address = branch.Address,
        City = branch.City,
        Phone = branch.Phone,
        Latitude = branch.Latitude,
        Longitude = branch.Longitude,
        IsActive = branch.IsActive,
        CreatedAt = branch.CreatedAt,
        UpdatedAt = branch.UpdatedAt
    };
}
