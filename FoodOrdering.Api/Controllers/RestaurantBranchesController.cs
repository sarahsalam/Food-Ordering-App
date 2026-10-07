using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
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
        return result.Exists ? Ok(new { message = "Branches retrieved successfully.", data = result.Data }) : NotFound(new { message = "Restaurant not found." });
    }

    [HttpGet("branches/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var branch = await _mediator.Send(new GetBranchByIdQuery(id));
        return branch == null ? NotFound(new { message = "Branch not found." }) : Ok(new { message = "Branch retrieved successfully.", data = branch });
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
            return Unauthorized(new { message = "Sign in to manage restaurant branches." });
        }

        var result = await _mediator.Send(
            new CreateBranchCommand(restaurantId, ownerId, request));
        if (!result.Success)
        {
            if (result.Error == "You do not own this restaurant.")
            {
                return StatusCode(403, new { message = "You do not have permission to create this branch." });
            }

            return NotFound(new { message = result.Error ?? "Unable to complete Create for RestaurantBranches." });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            new { message = "Branch created successfully.", data = result.Data });
    }

    [HttpPut("branches/{id:guid}")]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> Update(Guid id, UpdateBranchRequest request)
    {
        var result = await _mediator.Send(new UpdateBranchCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Update for RestaurantBranches." });
        }

        return Ok(new { message = "Branch updated successfully.", data = result.Data });
    }

    [HttpDelete("branches/{id:guid}")]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteBranchCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Delete for RestaurantBranches." });
        }

        return Ok(new { message = "Branch deleted successfully." });
    }
}
