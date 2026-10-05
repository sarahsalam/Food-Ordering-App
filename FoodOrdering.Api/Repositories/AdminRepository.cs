using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Security;
using FoodOrdering.Api.Data;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _context;

    public AdminRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IdentityRole<Guid>?> GetRoleAsync(Guid id)
    {
        return _context.Roles.SingleOrDefaultAsync(item => item.Id == id);
    }

    public Task<List<IdentityRole<Guid>>> GetRolesAsync()
    {
        return _context.Roles
            .AsNoTracking()
            .OrderBy(role => role.Name)
            .ToListAsync();
    }

    public Task<bool> UserExistsAsync(Guid userId)
    {
        return _context.Users.AnyAsync(user => user.Id == userId);
    }

    public Task<List<IdentityRole<Guid>>> GetUserRolesAsync(Guid userId)
    {
        return _context.Roles
            .AsNoTracking()
            .Where(role => _context.UserRoles.Any(item => item.UserId == userId && item.RoleId == role.Id))
            .ToListAsync();
    }

    public Task<bool> RestaurantExistsAsync(Guid restaurantId)
    {
        return _context.Restaurants.AnyAsync(item => item.Id == restaurantId);
    }

    public Task<bool> IsStaffAssignedAsync(Guid restaurantId, Guid userId)
    {
        return _context.RestaurantStaff.AnyAsync(item => item.UserId == userId && item.RestaurantId == restaurantId);
    }

    public Task<List<Restaurant>> GetRestaurantsAsync(RestaurantApprovalStatus approvalStatus)
    {
        return _context.Restaurants
            .AsNoTracking()
            .Include(item => item.Branches)
            .Where(item => item.ApprovalStatus == approvalStatus)
            .ToListAsync();
    }

    public Task<Restaurant?> GetRestaurantAsync(Guid id)
    {
        return _context.Restaurants.Include(item => item.Branches).SingleOrDefaultAsync(item => item.Id == id);
    }

    public void AddRestaurantStaff(RestaurantStaff assignment)
    {
        _context.RestaurantStaff.Add(assignment);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
