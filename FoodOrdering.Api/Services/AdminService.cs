using System.Security.Claims;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;
using FoodOrdering.Api.Security;

namespace FoodOrdering.Api.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _repository;
    private readonly UserManager<ApplicationUser> _users;
    private readonly IUserAccessRepository _accessRepository;

    public AdminService(IAdminRepository repository, UserManager<ApplicationUser> users,
        IUserAccessRepository accessRepository)
    {
        _repository = repository;
        _users = users;
        _accessRepository = accessRepository;
    }

    public async Task<ServiceResult> AddRoleAsync(Guid userId, AssignRoleRequest request, ClaimsPrincipal actor)
    {
        var role = await _repository.GetRoleAsync(request.RoleId);
        if (role == null || role.Name == null)
        {
            return ServiceResult.Invalid(new { message = "Select a valid roleId from GET /api/admin/roles." });
        }
        var user = await _users.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return ServiceResult.Missing();
        }
        if (!await _users.IsInRoleAsync(user, role.Name))
        {
            var result = await _users.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                return ServiceResult.Invalid(new { errors = result.Errors.Select(error => error.Description) });
            }
        }
        return ServiceResult.Ok(new { message = "Role assigned. Reload the permissions endpoint." });
    }

    public async Task<ServiceResult> RolesAsync(ClaimsPrincipal actor)
    {
        return ServiceResult.Ok((await _repository.GetRolesAsync()).Select(role => new { role.Id, role.Name }).ToList());
    }

    public async Task<ServiceResult> UserRolesAsync(Guid userId, ClaimsPrincipal actor)
    {
        if (!await _repository.UserExistsAsync(userId))
        {
            return ServiceResult.Missing();
        }
        var roles = (await _repository.GetUserRolesAsync(userId)).Select(role => new { role.Id, role.Name }).ToList();
        return ServiceResult.Ok(roles);
    }

    public async Task<ServiceResult> RemoveRoleAsync(Guid userId, Guid roleId, ClaimsPrincipal actor)
    {
        var role = await _repository.GetRoleAsync(roleId);
        var user = await _users.FindByIdAsync(userId.ToString());
        if (user == null || role == null || role.Name == null)
        {
            return ServiceResult.Missing();
        }
        if (await _users.IsInRoleAsync(user, role.Name))
        {
            var result = await _users.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                return ServiceResult.Invalid(new { errors = result.Errors.Select(error => error.Description) });
            }
        }
        return ServiceResult.Ok(new { message = "Role removed. Reload the permissions endpoint." });
    }

    public async Task<ServiceResult> AssignStaffAsync(Guid restaurantId, Guid userId, ClaimsPrincipal actor)
    {
        var user = await _users.FindByIdAsync(userId.ToString());
        if (user == null || !await _repository.RestaurantExistsAsync(restaurantId))
        {
            return ServiceResult.Missing();
        }
        if (!(await _accessRepository.GetPermissionsAsync(userId)).Contains(PermissionNames.RestaurantOrdersManage))
        {
            return ServiceResult.Invalid(new { message = "Assign the can_manage_restaurant_orders permission to this user first." });
        }
        if (!await _repository.IsStaffAssignedAsync(restaurantId, userId))
        {
            _repository.AddRestaurantStaff(new RestaurantStaff { UserId = userId, RestaurantId = restaurantId });
            await _repository.SaveChangesAsync();
        }
        return ServiceResult.Ok(new { message = "Staff assigned to restaurant." });
    }

    public async Task<ServiceResult> RestaurantsAsync(RestaurantApprovalStatus approvalStatus, ClaimsPrincipal actor)
    {
        if (!Enum.IsDefined(approvalStatus))
        {
            return ServiceResult.Invalid(new { message = "Invalid approval status." });
        }
        var restaurants = await _repository.GetRestaurantsAsync(approvalStatus);
        return ServiceResult.Ok(restaurants.Select(RestaurantService.Map));
    }

    public Task<ServiceResult> ApproveAsync(Guid id, ReviewRestaurantRequest request, ClaimsPrincipal actor)
    {
        return ReviewAsync(id, RestaurantApprovalStatus.Approved, request.Note, actor);
    }

    public Task<ServiceResult> RejectAsync(Guid id, ReviewRestaurantRequest request, ClaimsPrincipal actor)
    {
        return ReviewAsync(id, RestaurantApprovalStatus.Rejected, request.Note, actor);
    }

    public Task<ServiceResult> SuspendAsync(Guid id, ReviewRestaurantRequest request, ClaimsPrincipal actor)
    {
        return ReviewAsync(id, RestaurantApprovalStatus.Suspended, request.Note, actor);
    }

    private async Task<ServiceResult> ReviewAsync(Guid id, RestaurantApprovalStatus status, string? note, ClaimsPrincipal actor)
    {
        var restaurant = await _repository.GetRestaurantAsync(id);
        if (restaurant == null)
        {
            return ServiceResult.Missing();
        }
        bool validTransition = false;
        if (status == RestaurantApprovalStatus.Approved)
        {
            validTransition = restaurant.ApprovalStatus == RestaurantApprovalStatus.Pending ||
                              restaurant.ApprovalStatus == RestaurantApprovalStatus.Suspended;
        }
        if (status == RestaurantApprovalStatus.Rejected)
        {
            validTransition = restaurant.ApprovalStatus == RestaurantApprovalStatus.Pending;
        }
        if (status == RestaurantApprovalStatus.Suspended)
        {
            validTransition = restaurant.ApprovalStatus == RestaurantApprovalStatus.Approved;
        }
        if (!validTransition)
        {
            return ServiceResult.Conflict(new { message = "This review action is not allowed for the restaurant's current status." });
        }
        restaurant.ApprovalStatus = status;
        restaurant.IsActive = status == RestaurantApprovalStatus.Approved;
        restaurant.ReviewedByUserId = Guid.Parse(actor.FindFirstValue(ClaimTypes.NameIdentifier)!);
        restaurant.ReviewedAt = DateTime.UtcNow;
        restaurant.ReviewNote = note;
        restaurant.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return ServiceResult.Ok(RestaurantService.Map(restaurant));
    }
}
