using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FoodOrdering.Api.Services;

public class RbacService(RbacRepository repository, RoleManager<IdentityRole<Guid>> roleManager)
{
    public async Task<ServiceResult> ListPermissionsAsync() => ServiceResult.Ok(await repository.PermissionsAsync());

    public async Task<ServiceResult> ListRolesAsync() =>
        ServiceResult.Ok((await repository.RolesAsync()).Select(x => new { x.Id, x.Name }));

    public async Task<ServiceResult> CreatePermissionAsync(string name)
    {
        if ((await repository.PermissionsAsync()).Any(x => x.Name == name))
            return ServiceResult.Conflict(new { message = "Permission already exists." });
        var permission = new Permission { Id = Guid.NewGuid(), Name = name };
        repository.AddPermission(permission);
        try
        {
            await repository.SaveAsync();
        }
        catch (DbUpdateException error) when (error.InnerException is PostgresException
                                              {
                                                  SqlState: PostgresErrorCodes.UniqueViolation
                                              })
        {
            return ServiceResult.Conflict(new { message = "Permission already exists." });
        }

        return ServiceResult.Created(permission);
    }

    public async Task<ServiceResult> CreateRoleAsync(string name)
    {
        var role = new IdentityRole<Guid>(name) { Id = Guid.NewGuid() };
        IdentityResult result;
        try
        {
            result = await roleManager.CreateAsync(role);
        }
        catch (DbUpdateException error) when (error.InnerException is PostgresException
                                              {
                                                  SqlState: PostgresErrorCodes.UniqueViolation
                                              })
        {
            return ServiceResult.Conflict(new { message = "Role already exists." });
        }

        if (!result.Succeeded)
            return result.Errors.Any(x => x.Code == "DuplicateRoleName")
                ? ServiceResult.Conflict(new { message = "Role already exists." })
                : ServiceResult.Invalid(new { errors = result.Errors.Select(x => x.Description) });
        return ServiceResult.Created(new { role.Id, role.Name });
    }

    public async Task<ServiceResult> DeletePermissionAsync(Guid id)
    {
        var permission = (await repository.PermissionsAsync()).SingleOrDefault(x => x.Id == id);
        if (permission == null) return ServiceResult.Missing();
        repository.RemovePermission(permission);
        await repository.SaveAsync();
        return ServiceResult.Ok(new { message = "Permission deleted successfully." });
    }

    public async Task<ServiceResult> DeleteRoleAsync(Guid id)
    {
        var role = (await repository.RolesAsync()).SingleOrDefault(x => x.Id == id);
        if (role == null) return ServiceResult.Missing();
        repository.RemoveRole(role);
        await repository.SaveAsync();
        return ServiceResult.Ok(new { message = "Role deleted successfully." });
    }

    public async Task<ServiceResult> RoleAsync(Guid id)
    {
        var role = (await repository.RolesAsync()).SingleOrDefault(x => x.Id == id);
        if (role == null) return ServiceResult.Missing();
        var ids = (await repository.RolePermissionsAsync(id)).Select(x => x.PermissionId).ToHashSet();
        return ServiceResult.Ok(new
            { role.Id, role.Name, permissions = (await repository.PermissionsAsync()).Where(x => ids.Contains(x.Id)) });
    }

    public async Task<ServiceResult> AssignPermissionsAsync(Guid id, List<string> values)
    {
        var role = (await repository.RolesAsync()).SingleOrDefault(x => x.Id == id);
        if (role == null) return ServiceResult.Missing();
        var permissions = await repository.PermissionsAsync();
        var ids = Resolve(values, permissions.Select(x => (x.Id, x.Name)), StringComparer.Ordinal);
        if (ids == null)
            return ServiceResult.Invalid(new { message = "Every permission must be an existing ID or name." });
        await repository.AssignPermissionsAsync(id, ids);
        return ServiceResult.Ok(new
        {
            message = $"Permissions assigned to role: {role.Name}", role_id = id, permission_ids = ids
        });
    }

    public async Task<ServiceResult> RevokePermissionAsync(Guid id, Guid permissionId)
    {
        if (!(await repository.RolesAsync()).Any(x => x.Id == id) ||
            !(await repository.PermissionsAsync()).Any(x => x.Id == permissionId))
            return ServiceResult.Missing();
        await repository.RemovePermissionFromRoleAsync(id, permissionId);
        return ServiceResult.Ok(new { message = "Permission removed from role successfully." });
    }

    public async Task<ServiceResult> AssignRolesAsync(Guid id, List<string> values)
    {
        if (!await repository.UserExistsAsync(id)) return ServiceResult.Missing();
        var roles = await repository.RolesAsync();
        var ids = Resolve(values, roles.Select(x => (x.Id, x.Name!)), StringComparer.OrdinalIgnoreCase);
        if (ids == null) return ServiceResult.Invalid(new { message = "Every role must be an existing ID or name." });
        await repository.AssignRolesAsync(id, ids);
        return ServiceResult.Ok(new
        {
            message = $"Roles assigned to user {id}", user_id = id,
            roles = roles.Where(x => ids.Contains(x.Id)).Select(x => x.Name)
        });
    }

    public async Task<ServiceResult> UserRolesAsync(Guid id)
    {
        if (!await repository.UserExistsAsync(id)) return ServiceResult.Missing();
        var ids = (await repository.UserRolesAsync(id)).Select(x => x.RoleId).ToHashSet();
        var roles = (await repository.RolesAsync()).Where(x => ids.Contains(x.Id)).ToList();
        var permissionIds = new HashSet<Guid>();
        foreach (var role in roles)
            permissionIds.UnionWith((await repository.RolePermissionsAsync(role.Id)).Select(x => x.PermissionId));
        return ServiceResult.Ok(new
        {
            user_id = id, roles = roles.Select(x => x.Name),
            effective_permissions = (await repository.PermissionsAsync()).Where(x => permissionIds.Contains(x.Id))
                .Select(x => x.Name)
        });
    }

    public async Task<ServiceResult> RevokeRoleAsync(Guid id, Guid roleId)
    {
        if (!await repository.UserExistsAsync(id) || !(await repository.RolesAsync()).Any(x => x.Id == roleId))
            return ServiceResult.Missing();
        await repository.RemoveRoleFromUserAsync(id, roleId);
        return ServiceResult.Ok(new { message = "Role removed from user successfully." });
    }

    private static List<Guid>? Resolve(List<string> values, IEnumerable<(Guid Id, string Name)> choices,
        StringComparer comparer)
    {
        var items = choices.ToList();
        var ids = new HashSet<Guid>();
        foreach (var value in values)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            var match = items.FirstOrDefault(x =>
                comparer.Equals(x.Name, value) || (Guid.TryParse(value, out var id) && x.Id == id));
            if (match.Id == Guid.Empty) return null;
            ids.Add(match.Id);
        }

        return ids.Order().ToList();
    }
}