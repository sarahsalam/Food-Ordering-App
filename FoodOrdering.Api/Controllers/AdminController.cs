using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Security;
using FoodOrdering.Api.Services;
using FoodOrdering.Api.Mediator.Commands;
using FoodOrdering.Api.Mediator.Queries;
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
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpGet("roles")]
    public async Task<IActionResult> Roles()
    {
        var result = await _mediator.Send(new AdminRolesQuery(User));
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpGet("users/{userId:guid}/roles")]
    public async Task<IActionResult> UserRoles(Guid userId)
    {
        var result = await _mediator.Send(new AdminUserRolesQuery(userId, User));
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpDelete("users/{userId:guid}/roles/{roleId:guid}")]
    public async Task<IActionResult> RemoveRole(Guid userId, Guid roleId)
    {
        var result = await _mediator.Send(new AdminRemoveRoleCommand(userId, roleId, User));
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpPost("restaurants/{restaurantId:guid}/staff/{userId:guid}")]
    public async Task<IActionResult> AssignStaff(Guid restaurantId, Guid userId)
    {
        var result = await _mediator.Send(new AdminAssignStaffCommand(restaurantId, userId, User));
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.RestaurantReview)]
    [HttpGet("restaurants")]
    public async Task<IActionResult> Restaurants([FromQuery] RestaurantApprovalStatus approvalStatus = RestaurantApprovalStatus.Pending)
    {
        var result = await _mediator.Send(new AdminRestaurantsQuery(approvalStatus, User));
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.RestaurantReview)]
    [HttpPost("restaurants/{id:guid}/approve")]
    public async Task<IActionResult> Approve(Guid id, ReviewRestaurantRequest request)
    {
        var result = await _mediator.Send(new AdminApproveCommand(id, request, User));
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.RestaurantReview)]
    [HttpPost("restaurants/{id:guid}/reject")]
    public async Task<IActionResult> Reject(Guid id, ReviewRestaurantRequest request)
    {
        var result = await _mediator.Send(new AdminRejectCommand(id, request, User));
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.RestaurantReview)]
    [HttpPost("restaurants/{id:guid}/suspend")]
    public async Task<IActionResult> Suspend(Guid id, ReviewRestaurantRequest request)
    {
        var result = await _mediator.Send(new AdminSuspendCommand(id, request, User));
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpPost("roles/{roleId:guid}/permissions")]
    public async Task<IActionResult> AssignPermission(Guid roleId, AssignPermissionRequest request)
    {
        var result = await _mediator.Send(new AssignRolePermissionCommand(roleId, request.PermissionId));
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpDelete("roles/{roleId:guid}/permissions/{permissionId:guid}")]
    public async Task<IActionResult> RemovePermission(Guid roleId, Guid permissionId)
    {
        var result = await _mediator.Send(new RemoveRolePermissionCommand(roleId, permissionId));
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpGet("roles/{roleId:guid}/permissions")]
    public async Task<IActionResult> RolePermissions(Guid roleId)
    {
        var result = await _mediator.Send(new RolePermissionsQuery(roleId));
        return ToResponse(result);
    }

    private IActionResult ToResponse(ServiceResult result)
    {
        switch (result.Outcome)
        {
            case ServiceOutcome.Success:
                return Ok(result.Data);
            case ServiceOutcome.Created:
                return StatusCode(201, result.Data);
            case ServiceOutcome.Invalid:
                return BadRequest(result.Data);
            case ServiceOutcome.Conflict:
                return Conflict(result.Data);
            case ServiceOutcome.NotFound:
                return result.Data == null ? NotFound() : NotFound(result.Data);
            default:
                throw new InvalidOperationException("Unknown service outcome.");
        }
    }
}
