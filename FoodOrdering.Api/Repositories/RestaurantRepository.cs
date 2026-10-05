using FoodOrdering.Api.Data;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _context;

    public RestaurantRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Restaurant>> GetAllAsync(Guid ownerId, RestaurantQueryRequest query)
    {
        var restaurants = _context.Restaurants
            .Where(restaurant => restaurant.OwnerId == ownerId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = $"%{query.Search.Trim()}%";
            restaurants = restaurants.Where(restaurant =>
                EF.Functions.ILike(restaurant.Name, search) ||
                (restaurant.Description != null &&
                 EF.Functions.ILike(restaurant.Description, search)) ||
                EF.Functions.ILike(restaurant.Phone, search) ||
                (restaurant.Email != null &&
                 EF.Functions.ILike(restaurant.Email, search)));
        }

        if (query.IsActive.HasValue)
        {
            restaurants = restaurants.Where(restaurant =>
                restaurant.IsActive == query.IsActive.Value);
        }

        return restaurants
            .Include(restaurant => restaurant.Branches)
            .ToListAsync();
    }

    public Task<Restaurant?> GetByIdAsync(Guid ownerId, Guid id) =>
        _context.Restaurants
            .Include(restaurant => restaurant.Branches)
            .FirstOrDefaultAsync(restaurant =>
                restaurant.Id == id && restaurant.OwnerId == ownerId);

    public Task<Restaurant?> GetByIdAsync(Guid id) =>
        _context.Restaurants
            .Include(restaurant => restaurant.Branches)
            .FirstOrDefaultAsync(restaurant => restaurant.Id == id);

    public Task<bool> OwnerExistsAsync(Guid ownerId) =>
        _context.Users.AnyAsync(user => user.Id == ownerId);

    public Task AddAsync(Restaurant restaurant) =>
        _context.Restaurants.AddAsync(restaurant).AsTask();

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
    public Task<List<Restaurant>> GetAvailableAsync()
    {
        return _context.Restaurants.AsNoTracking().Where(item => item.IsActive && item.ApprovalStatus == RestaurantApprovalStatus.Approved).ToListAsync();
    }
}