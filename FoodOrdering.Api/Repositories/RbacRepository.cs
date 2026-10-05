using FoodOrdering.Api.Data;
using FoodOrdering.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class RbacRepository(AppDbContext context)
{
    public Task<List<Permission>> PermissionsAsync() => context.Permissions.OrderBy(x => x.Name).ToListAsync();
    public Task<List<IdentityRole<Guid>>> RolesAsync() => context.Roles.OrderBy(x => x.Name).ToListAsync();
    public Task<bool> UserExistsAsync(Guid id) => context.Users.AnyAsync(x => x.Id == id);

    public Task<List<RolePermission>> RolePermissionsAsync(Guid id) =>
        context.RolePermissions.Where(x => x.RoleId == id).ToListAsync();

    public Task<List<IdentityUserRole<Guid>>> UserRolesAsync(Guid id) =>
        context.UserRoles.Where(x => x.UserId == id).ToListAsync();

    public void AddPermission(Permission permission) => context.Permissions.Add(permission);
    public void RemovePermission(Permission permission) => context.Permissions.Remove(permission);
    public void RemoveRole(IdentityRole<Guid> role) => context.Roles.Remove(role);

    public async Task AssignPermissionsAsync(Guid id, List<Guid> ids)
    {
        var existing = await RolePermissionsAsync(id);
        context.RolePermissions.AddRange(ids.Where(value => existing.All(x => x.PermissionId != value))
            .Select(value => new RolePermission { RoleId = id, PermissionId = value }));
        await context.SaveChangesAsync();
    }

    public async Task AssignRolesAsync(Guid id, List<Guid> ids)
    {
        var existing = await UserRolesAsync(id);
        context.UserRoles.AddRange(ids.Where(value => existing.All(x => x.RoleId != value))
            .Select(value => new IdentityUserRole<Guid> { UserId = id, RoleId = value }));
        await context.SaveChangesAsync();
    }

    public async Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId)
    {
        var assignment =
            await context.RolePermissions.SingleOrDefaultAsync(x =>
                x.RoleId == roleId && x.PermissionId == permissionId);
        if (assignment == null) return;
        context.RolePermissions.Remove(assignment);
        await context.SaveChangesAsync();
    }

    public async Task RemoveRoleFromUserAsync(Guid userId, Guid roleId)
    {
        var assignment = await context.UserRoles.SingleOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);
        if (assignment == null) return;
        context.UserRoles.Remove(assignment);
        await context.SaveChangesAsync();
    }

    public async Task SaveAsync() => await context.SaveChangesAsync();
}