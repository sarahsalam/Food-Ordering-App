using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Infrastructure.Repositories;

public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly AppDbContext _context;

    public RolePermissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> RoleExistsAsync(Guid roleId)
    {
        return _context.Roles.AnyAsync(role => role.Id == roleId);
    }

    public Task<bool> PermissionExistsAsync(Guid permissionId)
    {
        return _context.Permissions.AnyAsync(permission => permission.Id == permissionId);
    }

    public Task<RolePermission?> GetAsync(Guid roleId, Guid permissionId)
    {
        return _context.RolePermissions.SingleOrDefaultAsync(item => item.RoleId == roleId && item.PermissionId == permissionId);
    }

    public Task<List<PermissionResponse>> GetByRoleAsync(Guid roleId)
    {
        return _context.RolePermissions.AsNoTracking()
            .Where(item => item.RoleId == roleId)
            .OrderBy(item => item.Permission.Name)
            .Select(item => new PermissionResponse(item.PermissionId, item.Permission.Name))
            .ToListAsync();
    }

    public void Add(RolePermission assignment)
    {
        _context.RolePermissions.Add(assignment);
    }

    public void Remove(RolePermission assignment)
    {
        _context.RolePermissions.Remove(assignment);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
