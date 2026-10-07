using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Route("api")]
public class AddonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("addons")]
    public async Task<IActionResult> Get([FromQuery] string? search, [FromQuery] bool? isActive)
    {
        return Ok(new { message = "Addons retrieved successfully.", data = await _mediator.Send(new AddonsQuery(new(search, isActive))) });
    }

    [HttpGet("addons/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new AddonQuery(id));
        return result == null ? NotFound(new { message = "Addon not found." }) : Ok(new { message = "Addon retrieved successfully.", data = result });
    }

    [HttpPost("restaurants/{restaurantId:guid}/addons")]
    [Authorize(Policy = PermissionNames.AddonsManage)]
    public async Task<IActionResult> Create(Guid restaurantId, CreateAddonRequest request)
    {
        var result = await _mediator.Send(new CreateAddonCommand(restaurantId, request));
        if (!result.Success)
        {
            return BadRequest(new { message = result.Error ?? "Unable to complete Create for Addons." });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            new { message = "Addon created successfully.", data = result.Data });
    }

    [HttpPut("addons/{id:guid}")]
    [Authorize(Policy = PermissionNames.AddonsManage)]
    public async Task<IActionResult> Update(Guid id, UpdateAddonRequest request)
    {
        var result = await _mediator.Send(new UpdateAddonCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Update for Addons." });
        }

        return Ok(new { message = "Addon updated successfully.", data = result.Data });
    }

    [HttpDelete("addons/{id:guid}")]
    [Authorize(Policy = PermissionNames.AddonsManage)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteAddonCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Delete for Addons." });
        }

        return Ok(new { message = "Addon deleted successfully." });
    }
}
