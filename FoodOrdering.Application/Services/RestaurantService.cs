using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _repository;

    public RestaurantService(IRestaurantRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RestaurantResponse>> GetAllAsync(
        Guid ownerId,
        RestaurantQueryRequest query) =>
        (await _repository.GetAllAsync(ownerId, query)).Select(Map).ToList();

    public async Task<RestaurantResponse?> GetByIdAsync(Guid ownerId, Guid id)
    {
        var restaurant = await _repository.GetByIdAsync(ownerId, id);
        return restaurant == null ? null : Map(restaurant);
    }

    public async Task<(bool Success, string? Error, RestaurantResponse? Data)> CreateAsync(
        Guid ownerId,
        CreateRestaurantRequest request)
    {
        if (!await _repository.OwnerExistsAsync(ownerId))
        {
            return (false, "Owner was not found.", null);
        }

        var restaurant = new Restaurant
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Phone = request.Phone,
            Email = request.Email,
            LogoUrl = request.LogoUrl,
            IsActive = false,
            ApprovalStatus = RestaurantApprovalStatus.Pending,
            OwnerId = ownerId,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(restaurant);
        await _repository.SaveChangesAsync();
        return (true, null, Map(restaurant));
    }

    public async Task<(bool Success, string? Error, RestaurantResponse? Data)> UpdateAsync(
        Guid id,
        UpdateRestaurantRequest request)
    {
        var restaurant = await _repository.GetByIdAsync(id);
        if (restaurant == null)
        {
            return (false, "Restaurant was not found.", null);
        }

        restaurant.Name = request.Name;
        restaurant.Description = request.Description;
        restaurant.Phone = request.Phone;
        restaurant.Email = request.Email;
        restaurant.LogoUrl = request.LogoUrl;
        restaurant.IsActive = request.IsActive &&
                              restaurant.ApprovalStatus == RestaurantApprovalStatus.Approved;
        restaurant.UpdatedAt = DateTime.UtcNow;

        await _repository.SaveChangesAsync();
        return (true, null, Map(restaurant));
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(Guid id)
    {
        var restaurant = await _repository.GetByIdAsync(id);
        if (restaurant == null)
        {
            return (false, "Restaurant was not found.");
        }

        restaurant.IsActive = false;
        restaurant.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null);
    }

    public static RestaurantResponse Map(Restaurant restaurant) => new()
    {
        Id = restaurant.Id,
        Name = restaurant.Name,
        Description = restaurant.Description,
        Phone = restaurant.Phone,
        Email = restaurant.Email,
        LogoUrl = restaurant.LogoUrl,
        IsActive = restaurant.IsActive,
        OwnerId = restaurant.OwnerId,
        ApprovalStatus = restaurant.ApprovalStatus.ToString(),
        ReviewNote = restaurant.ReviewNote,
        ReviewedByUserId = restaurant.ReviewedByUserId,
        ReviewedAt = restaurant.ReviewedAt,
        CreatedAt = restaurant.CreatedAt,
        UpdatedAt = restaurant.UpdatedAt,
        Branches = restaurant.Branches.Select(RestaurantBranchService.Map).ToList()
    };
}
