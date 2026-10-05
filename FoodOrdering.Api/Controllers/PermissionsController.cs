using FoodOrdering.Api.Mediator.Queries;
using FoodOrdering.Api.Mediator.Commands;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/permissions")]
public class PermissionsController : RbacControllerBase
{
    private readonly IMediator _mediator;

    public PermissionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("me")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public async Task<IActionResult> Get()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
        }
        var result = await _mediator.Send(new PermissionsQuery(userId));
        return Ok(result);
    }
    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpGet]
    public async Task<IActionResult> List() => ToRbacResponse(await _mediator.Send(new GetAllPermissionsQuery()));

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateRbacNameRequest request) =>
        ToRbacResponse(await _mediator.Send(new CreatePermissionCommand(request.Name)));

    [Authorize(Policy = PermissionNames.UsersManageRoles)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) =>
        ToRbacResponse(await _mediator.Send(new DeletePermissionCommand(id)));
}
