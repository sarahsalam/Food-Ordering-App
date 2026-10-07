
namespace FoodOrdering.Application.Services;

public class RolePermissionService : IRolePermissionService
{
    private readonly IRolePermissionRepository _repository;

    public RolePermissionService(IRolePermissionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult> AssignAsync(Guid roleId, Guid permissionId)
    {
        if (roleId == Guid.Empty || permissionId == Guid.Empty)
        {
            return ServiceResult.Invalid(new { message = "Role ID and permission ID are required." });
        }
        if (!await _repository.RoleExistsAsync(roleId))
        {
            return ServiceResult.Missing(new { message = "Role was not found." });
        }
        if (!await _repository.PermissionExistsAsync(permissionId))
        {
            return ServiceResult.Missing(new { message = "Permission was not found." });
        }

        var assignment = await _repository.GetAsync(roleId, permissionId);
        if (assignment != null)
        {
            return ServiceResult.Ok(new { roleId, permissionId });
        }

        _repository.Add(new RolePermission { RoleId = roleId, PermissionId = permissionId });
        await _repository.SaveChangesAsync();
        return ServiceResult.Created(new { roleId, permissionId });
    }

    public async Task<ServiceResult> RemoveAsync(Guid roleId, Guid permissionId)
    {
        if (!await _repository.RoleExistsAsync(roleId))
        {
            return ServiceResult.Missing(new { message = "Role was not found." });
        }
        if (!await _repository.PermissionExistsAsync(permissionId))
        {
            return ServiceResult.Missing(new { message = "Permission was not found." });
        }

        var assignment = await _repository.GetAsync(roleId, permissionId);
        if (assignment != null)
        {
            _repository.Remove(assignment);
            await _repository.SaveChangesAsync();
        }

        return ServiceResult.Ok(new { message = "Permission removed from role." });
    }

    public async Task<ServiceResult> GetByRoleAsync(Guid roleId)
    {
        if (!await _repository.RoleExistsAsync(roleId))
        {
            return ServiceResult.Missing(new { message = "Role was not found." });
        }

        return ServiceResult.Ok(await _repository.GetByRoleAsync(roleId));
    }
}
