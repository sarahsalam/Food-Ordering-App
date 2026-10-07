using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
using FoodOrdering.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpPost("users/{userId:guid}/roles")]
    public async Task<IActionResult> AddRole(Guid userId, AssignRoleRequest request)
    {
        var result = await _mediator.Send(new AdminAddRoleCommand(userId, request, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Role assigned to user successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Role assigned to user successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to assign the role to this user.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpGet("roles")]
    public async Task<IActionResult> Roles()
    {
        var result = await _mediator.Send(new AdminRolesQuery(User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Roles retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Roles retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Roles: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Roles: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to retrieve roles.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpGet("users/{userId:guid}/roles")]
    public async Task<IActionResult> UserRoles(Guid userId)
    {
        var result = await _mediator.Send(new AdminUserRolesQuery(userId, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "User roles retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "User roles retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "User roles: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "User roles: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "User not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpDelete("users/{userId:guid}/roles/{roleId:guid}")]
    public async Task<IActionResult> RemoveRole(Guid userId, Guid roleId)
    {
        var result = await _mediator.Send(new AdminRemoveRoleCommand(userId, roleId, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Role removed from user successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Role removed from user successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "User or role not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpPost("restaurants/{restaurantId:guid}/staff/{userId:guid}")]
    public async Task<IActionResult> AssignStaff(Guid restaurantId, Guid userId)
    {
        var result = await _mediator.Send(new AdminAssignStaffCommand(restaurantId, userId, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Restaurant staff assigned successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Restaurant staff assigned successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurant staff: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurant staff: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to assign restaurant staff.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.RestaurantReview)]
    [HttpGet("restaurants")]
    public async Task<IActionResult> Restaurants([FromQuery] RestaurantApprovalStatus approvalStatus = RestaurantApprovalStatus.Pending)
    {
        var result = await _mediator.Send(new AdminRestaurantsQuery(approvalStatus, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Restaurants retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Restaurants retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurants: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurants: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to retrieve restaurants.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.RestaurantReview)]
    [HttpPost("restaurants/{id:guid}/approve")]
    public async Task<IActionResult> Approve(Guid id, ReviewRestaurantRequest request)
    {
        var result = await _mediator.Send(new AdminApproveCommand(id, request, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Restaurant approved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Restaurant approved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurant: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurant: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to approve this restaurant.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.RestaurantReview)]
    [HttpPost("restaurants/{id:guid}/reject")]
    public async Task<IActionResult> Reject(Guid id, ReviewRestaurantRequest request)
    {
        var result = await _mediator.Send(new AdminRejectCommand(id, request, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Restaurant rejected successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Restaurant rejected successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurant: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurant: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to reject this restaurant.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.RestaurantReview)]
    [HttpPost("restaurants/{id:guid}/suspend")]
    public async Task<IActionResult> Suspend(Guid id, ReviewRestaurantRequest request)
    {
        var result = await _mediator.Send(new AdminSuspendCommand(id, request, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Restaurant suspended successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Restaurant suspended successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurant: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurant: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to suspend this restaurant.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpPost("roles/{roleId:guid}/permissions")]
    public async Task<IActionResult> AssignPermission(Guid roleId, AssignPermissionRequest request)
    {
        var result = await _mediator.Send(new AssignRolePermissionCommand(roleId, request.PermissionId));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Permission assigned to role successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Permission assigned to role successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Permission: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Permission: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to assign permission to this role.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpDelete("roles/{roleId:guid}/permissions/{permissionId:guid}")]
    public async Task<IActionResult> RemovePermission(Guid roleId, Guid permissionId)
    {
        var result = await _mediator.Send(new RemoveRolePermissionCommand(roleId, permissionId));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Permission removed from role successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Permission removed from role successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Permission: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Permission: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role or permission not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpGet("roles/{roleId:guid}/permissions")]
    public async Task<IActionResult> RolePermissions(Guid roleId)
    {
        var result = await _mediator.Send(new RolePermissionsQuery(roleId));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Role permissions retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Role permissions retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role permissions: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role permissions: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

}
