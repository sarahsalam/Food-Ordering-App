using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Responses;

namespace FoodOrdering.Api.Repositories.Interfaces;

public interface IRolePermissionRepository
{
    Task<bool> RoleExistsAsync(Guid roleId);
    Task<bool> PermissionExistsAsync(Guid permissionId);
    Task<RolePermission?> GetAsync(Guid roleId, Guid permissionId);
    Task<List<PermissionResponse>> GetByRoleAsync(Guid roleId);
    void Add(RolePermission assignment);
    void Remove(RolePermission assignment);
    Task SaveChangesAsync();
}
