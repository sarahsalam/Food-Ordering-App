using FoodOrdering.Api.Data;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class UserAccessRepository : IUserAccessRepository
{
    private readonly AppDbContext _context;

    public UserAccessRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<string>> GetRolesAsync(Guid userId)
    {
        return _context.Roles.AsNoTracking()
            .Where(role => _context.UserRoles.Any(item => item.UserId == userId && item.RoleId == role.Id))
            .Select(role => role.Name!).ToListAsync();
    }

    public Task<List<string>> GetPermissionsAsync(Guid userId)
    {
        return _context.RolePermissions.AsNoTracking()
            .Where(item => _context.UserRoles.Any(userRole => userRole.UserId == userId && userRole.RoleId == item.RoleId))
            .Select(item => item.Permission.Name).Distinct().ToListAsync();
    }
}
