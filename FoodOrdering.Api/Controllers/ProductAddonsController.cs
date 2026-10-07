using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Route("api")]
public class ProductAddonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductAddonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("products/{productId:guid}/addons")]
    public async Task<IActionResult> Get(Guid productId)
    {
        var result = await _mediator.Send(new ProductAddonsQuery(productId));
        return result.Exists ? Ok(new { message = "Product addon retrieved successfully.", data = result.Data }) : NotFound(new { message = "Product not found." });
    }

    [HttpPost("products/{productId:guid}/addons/{addonId:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Add(Guid productId, Guid addonId)
    {
        var result = await _mediator.Send(new AddProductAddonCommand(productId, addonId));
        if (!result.Success)
        {
            if (result.Error?.StartsWith("CONFLICT:") == true)
            {
                return Conflict(new { message = result.Error[9..] });
            }

            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error ?? "Unable to complete Add for ProductAddons." });
            }

            return BadRequest(new { message = result.Error ?? "Unable to complete Add for ProductAddons." });
        }

        return StatusCode(201, new { message = "Product addon added successfully.", data = result.Data });
    }

    [HttpDelete("products/{productId:guid}/addons/{addonId:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Remove(Guid productId, Guid addonId)
    {
        var result = await _mediator.Send(new DeleteProductAddonCommand(productId, addonId));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Remove for ProductAddons." });
        }

        return Ok(new { message = "Product addon deleted successfully." });
    }
}
