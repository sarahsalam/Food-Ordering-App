using FoodOrdering.Api.Data;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Order>> GetAsync(string? status, Guid? userId)
    {
        var query = _context.Orders.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(order => order.Status == status.Trim());
        }

        if (userId.HasValue)
        {
            query = query.Where(order => order.UserId == userId);
        }

        return query.ToListAsync();
    }

    public Task<Order?>
        GetByIdAsync(Guid id) => _context.Orders.AsNoTracking().FirstOrDefaultAsync(order => order.Id == id);

    public Task<Order?>
        GetTrackedByIdAsync(Guid id) => _context.Orders.FirstOrDefaultAsync(order => order.Id == id);

    public async Task<(bool User, bool Restaurant, bool Branch, bool BranchBelongs)> ValidateReferencesAsync(
        Guid userId,
        Guid restaurantId, Guid branchId)
    {
        var userExists = await _context.Users.AnyAsync(user => user.Id == userId);
        var restaurantExists = await _context.Restaurants.AnyAsync(restaurant => restaurant.Id == restaurantId);
        var branchExists = await _context.RestaurantBranches.AnyAsync(branch => branch.Id == branchId);
        var branchBelongs =
            await _context.RestaurantBranches.AnyAsync(branch =>
                branch.Id == branchId && branch.RestaurantId == restaurantId);

        return (userExists, restaurantExists, branchExists, branchBelongs);
    }

    public Task AddAsync(Order order) => _context.Orders.AddAsync(order).AsTask();
    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}