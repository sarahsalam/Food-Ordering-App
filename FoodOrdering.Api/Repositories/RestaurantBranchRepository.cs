using FoodOrdering.Api.Data;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class RestaurantBranchRepository : IRestaurantBranchRepository
{
    private readonly AppDbContext _context;

    public RestaurantBranchRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<RestaurantBranch>> GetByRestaurantIdAsync(
        Guid restaurantId,
        BranchQueryRequest query)
    {
        var branches = _context.RestaurantBranches
            .Where(branch => branch.RestaurantId == restaurantId);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = $"%{query.Search.Trim()}%";
            branches = branches.Where(branch =>
                EF.Functions.ILike(branch.Name, search) ||
                EF.Functions.ILike(branch.Address, search) ||
                EF.Functions.ILike(branch.City, search) ||
                (branch.Phone != null &&
                 EF.Functions.ILike(branch.Phone, search)));
        }

        if (!string.IsNullOrWhiteSpace(query.City))
        {
            var city = query.City.Trim();
            branches = branches.Where(branch =>
                EF.Functions.ILike(branch.City, city));
        }

        if (query.IsActive.HasValue)
        {
            branches = branches.Where(branch =>
                branch.IsActive == query.IsActive.Value);
        }

        return branches.ToListAsync();
    }

    public Task<RestaurantBranch?> GetByIdAsync(Guid id) =>
        _context.RestaurantBranches
            .FirstOrDefaultAsync(branch => branch.Id == id);

    public Task<bool> RestaurantExistsAsync(Guid restaurantId) =>
        _context.Restaurants.AnyAsync(restaurant => restaurant.Id == restaurantId);

    public Task<bool> RestaurantOwnedByUserAsync(Guid restaurantId, Guid ownerId) =>
        _context.Restaurants.AnyAsync(restaurant =>
            restaurant.Id == restaurantId && restaurant.OwnerId == ownerId);

    public Task AddAsync(RestaurantBranch branch) =>
        _context.RestaurantBranches.AddAsync(branch).AsTask();

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}