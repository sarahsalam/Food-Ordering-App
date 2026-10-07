using FoodOrdering.Application.DTOs.Requests;

namespace FoodOrdering.Application.Services.Interfaces;

public interface IAdminService
{
    Task<ServiceResult> AddRoleAsync(Guid userId, AssignRoleRequest request, ClaimsPrincipal actor);
    Task<ServiceResult> RolesAsync(ClaimsPrincipal actor);
    Task<ServiceResult> UserRolesAsync(Guid userId, ClaimsPrincipal actor);
    Task<ServiceResult> RemoveRoleAsync(Guid userId, Guid roleId, ClaimsPrincipal actor);
    Task<ServiceResult> AssignStaffAsync(Guid restaurantId, Guid userId, ClaimsPrincipal actor);
    Task<ServiceResult> RestaurantsAsync(RestaurantApprovalStatus approvalStatus, ClaimsPrincipal actor);
    Task<ServiceResult> ApproveAsync(Guid id, ReviewRestaurantRequest request, ClaimsPrincipal actor);
    Task<ServiceResult> RejectAsync(Guid id, ReviewRestaurantRequest request, ClaimsPrincipal actor);
    Task<ServiceResult> SuspendAsync(Guid id, ReviewRestaurantRequest request, ClaimsPrincipal actor);
}
