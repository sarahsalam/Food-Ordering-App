using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
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
            return Unauthorized(new { message = "Sign in to access restaurants." });
        }

        return Ok(new { message = "Restaurants retrieved successfully.", data = await _mediator.Send(new GetRestaurantsQuery(ownerId, query)) });
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var ownerIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(ownerIdClaim, out var ownerId))
        {
            return Unauthorized(new { message = "Sign in to access restaurants." });
        }

        var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(ownerId, id));
        return restaurant == null ? NotFound(new { message = "Restaurant not found." }) : Ok(new { message = "Restaurant retrieved successfully.", data = restaurant });
    }

    [HttpPost]
    [Authorize(Policy = PermissionNames.RestaurantCreate)]
    public async Task<IActionResult> Create(CreateRestaurantRequest request)
    {
        var ownerIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(ownerIdClaim, out var ownerId))
        {
            return Unauthorized(new { message = "Sign in to access restaurants." });
        }

        var result = await _mediator.Send(new CreateRestaurantCommand(ownerId, request));
        if (!result.Success)
        {
            return BadRequest(new { message = result.Error ?? "Unable to complete Create for Restaurants." });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            new { message = "Restaurant created successfully.", data = result.Data });
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> Update(Guid id, UpdateRestaurantRequest request)
    {
        var result = await _mediator.Send(new UpdateRestaurantCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Update for Restaurants." });
        }

        return Ok(new { message = "Restaurant updated successfully.", data = result.Data });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = PermissionNames.RestaurantManageOwn)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteRestaurantCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Delete for Restaurants." });
        }

        return Ok(new { message = "Restaurant deleted successfully." });
    }
}
