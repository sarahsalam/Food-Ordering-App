using FoodOrdering.Api.Security;
using FoodOrdering.Api.Mediator.Commands;
using FoodOrdering.Api.Mediator.Queries;
using FoodOrdering.Api.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> GetAll(
        [FromQuery] RestaurantQueryRequest query)
    {
        var ownerIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(ownerIdClaim, out var ownerId))
        {
            return Unauthorized();
        }

        return Ok(await _mediator.Send(new GetRestaurantsQuery(ownerId, query)));
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var ownerIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(ownerIdClaim, out var ownerId))
        {
            return Unauthorized();
        }

        var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(ownerId, id));
        return restaurant == null ? NotFound() : Ok(restaurant);
    }

    [HttpPost]
    [Authorize(Policy = PermissionNames.RestaurantCreate)]
    public async Task<IActionResult> Create(CreateRestaurantRequest request)
    {
        var ownerIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(ownerIdClaim, out var ownerId))
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(new CreateRestaurantCommand(ownerId, request));
        if (!result.Success)
        {
            return BadRequest(new { message = result.Error });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            result.Data);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> Update(Guid id, UpdateRestaurantRequest request)
    {
        var result = await _mediator.Send(new UpdateRestaurantCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteRestaurantCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(new { message = "Restaurant deleted successfully." });
    }
}