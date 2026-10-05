using FoodOrdering.Api.Security;
using FoodOrdering.Api.Mediator.Commands;
using FoodOrdering.Api.Mediator.Queries;
using FoodOrdering.Api.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Route("api")]
public class RestaurantBranchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantBranchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("restaurants/{restaurantId:guid}/branches")]
    public async Task<IActionResult> GetByRestaurant(
        Guid restaurantId,
        [FromQuery] BranchQueryRequest query)
    {
        var result = await _mediator.Send(
            new GetBranchesByRestaurantQuery(restaurantId, query));
        return result.Exists ? Ok(result.Data) : NotFound();
    }

    [HttpGet("branches/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var branch = await _mediator.Send(new GetBranchByIdQuery(id));
        return branch == null ? NotFound() : Ok(branch);
    }

    [HttpPost("restaurants/{restaurantId:guid}/branches")]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> Create(
        Guid restaurantId,
        CreateBranchRequest request)
    {
        var ownerIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(ownerIdClaim, out var ownerId))
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(
            new CreateBranchCommand(restaurantId, ownerId, request));
        if (!result.Success)
        {
            if (result.Error == "You do not own this restaurant.")
            {
                return Forbid();
            }

            return NotFound(new { message = result.Error });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            result.Data);
    }

    [HttpPut("branches/{id:guid}")]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> Update(Guid id, UpdateBranchRequest request)
    {
        var result = await _mediator.Send(new UpdateBranchCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(result.Data);
    }

    [HttpDelete("branches/{id:guid}")]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteBranchCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(new { message = "Branch deleted successfully." });
    }
}