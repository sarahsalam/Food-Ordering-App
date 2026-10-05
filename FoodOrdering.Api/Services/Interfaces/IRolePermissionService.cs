namespace FoodOrdering.Api.Services.Interfaces;

public interface IRolePermissionService
{
    Task<ServiceResult> AssignAsync(Guid roleId, Guid permissionId);
    Task<ServiceResult> RemoveAsync(Guid roleId, Guid permissionId);
    Task<ServiceResult> GetByRoleAsync(Guid roleId);
}
