using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Api.Security;
using FoodOrdering.Api.Mediator.Queries;
using FoodOrdering.Api.Mediator.Commands;
using FoodOrdering.Api.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[Authorize(Policy = PermissionNames.UsersManageRoles)]
[ApiController]
[Route("api/users")]
public class UsersController : RbacControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _mediator.Send(new GetUsersQuery()));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        return user == null ? NotFound() : Ok(user);
    }
    [HttpPost("{id:guid}/roles")]
    public async Task<IActionResult> AssignRoles(Guid id, AssignRolesRequest request) =>
        ToRbacResponse(await _mediator.Send(new AssignRolesToUserCommand(id, request.Roles!)));

    [HttpGet("{id:guid}/roles")]
    public async Task<IActionResult> Roles(Guid id) =>
        ToRbacResponse(await _mediator.Send(new GetUserAccessQuery(id)));

    [HttpDelete("{id:guid}/roles/{roleId:guid}")]
    public async Task<IActionResult> RevokeRole(Guid id, Guid roleId) =>
        ToRbacResponse(await _mediator.Send(new RevokeRoleFromUserCommand(id, roleId)));
}
