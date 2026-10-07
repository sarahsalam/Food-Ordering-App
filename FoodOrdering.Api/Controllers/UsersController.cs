using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[Authorize(Policy = PermissionNames.UsersManageRoles)]
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(new { message = "Users retrieved successfully.", data = await _mediator.Send(new GetUsersQuery()) });

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        return user == null ? NotFound(new { message = "User not found." }) : Ok(new { message = "User retrieved successfully.", data = user });
    }
    [HttpPost("{id:guid}/roles")]
    public async Task<IActionResult> AssignRoles(Guid id, AssignRolesRequest request)
    {
        var result = await _mediator.Send(new AssignRolesToUserCommand(id, request.Roles!));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Roles assigned to user successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Roles assigned to user successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Roles: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Roles: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to assign roles to this user.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpGet("{id:guid}/roles")]
    public async Task<IActionResult> Roles(Guid id)
    {
        var result = await _mediator.Send(new GetUserAccessQuery(id));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "User roles and permissions retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "User roles and permissions retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "User roles and permissions: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "User roles and permissions: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "User not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpDelete("{id:guid}/roles/{roleId:guid}")]
    public async Task<IActionResult> RevokeRole(Guid id, Guid roleId)
    {
        var result = await _mediator.Send(new RevokeRoleFromUserCommand(id, roleId));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Role revoked from user successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Role revoked from user successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Role: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "User or role not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }
}
