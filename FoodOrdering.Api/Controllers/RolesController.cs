using FoodOrdering.Api.Mediator.Queries;
using FoodOrdering.Api.Mediator.Commands;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Authorize(Policy = PermissionNames.UsersManageRoles)]
[Route("api/roles")]
public class RolesController(IMediator mediator) : RbacControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List() =>
        ToRbacResponse(await mediator.Send(new GetAllRolesQuery()));

    [HttpPost]
    public async Task<IActionResult> Create(CreateRbacNameRequest request) =>
        ToRbacResponse(await mediator.Send(new CreateRoleCommand(request.Name)));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) =>
        ToRbacResponse(await mediator.Send(new GetRoleDetailsQuery(id)));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) =>
        ToRbacResponse(await mediator.Send(new DeleteRoleCommand(id)));

    [HttpPost("{id:guid}/permissions")]
    public async Task<IActionResult> AssignPermissions(Guid id, AssignPermissionsRequest request) =>
        ToRbacResponse(
            await mediator.Send(new AssignPermissionsToRoleCommand(id, request.Permissions!)));

    [HttpDelete("{id:guid}/permissions/{permissionId:guid}")]
    public async Task<IActionResult> Revoke(Guid id, Guid permissionId) =>
        ToRbacResponse(await mediator.Send(new RevokePermissionFromRoleCommand(id, permissionId)));
}