using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Authorize(Policy = PermissionNames.UsersManageRoles)]
[Route("api/roles")]
public class RolesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var result = await mediator.Send(new GetAllRolesQuery());
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Roles retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Roles retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Roles: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Roles: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRbacNameRequest request)
    {
        var result = await mediator.Send(new CreateRoleCommand(request.Name));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Role created successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Role created successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await mediator.Send(new GetRoleDetailsQuery(id));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Role retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Role retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteRoleCommand(id));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Role deleted successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Role deleted successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role deleted successfully.: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role deleted successfully.: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpPost("{id:guid}/permissions")]
    public async Task<IActionResult> AssignPermissions(Guid id, AssignPermissionsRequest request)
    {
        var result = await mediator.Send(new AssignPermissionsToRoleCommand(id, request.Permissions!));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Permissions assigned to role successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Permissions assigned to role successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Permissions: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Permissions: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to assign permissions to this role.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpDelete("{id:guid}/permissions/{permissionId:guid}")]
    public async Task<IActionResult> Revoke(Guid id, Guid permissionId)
    {
        var result = await mediator.Send(new RevokePermissionFromRoleCommand(id, permissionId));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Permission revoked from role successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Permission revoked from role successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Permission: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Permission: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role or permission not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }
}
