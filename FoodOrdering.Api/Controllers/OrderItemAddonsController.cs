using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[Authorize(Policy = PermissionNames.OrderViewOwn)]
[ApiController]
[Route("api")]
public class OrderItemAddonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderItemAddonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("order-items/{orderItemId:guid}/addons")]
    public async Task<IActionResult> GetByItem(Guid orderItemId)
    {
        var result = await _mediator.Send(new OrderItemAddonsQuery(orderItemId));
        return result.Exists ? Ok(new { message = "Order item addons retrieved successfully.", data = result.Data }) : NotFound(new { message = "Order item not found." });
    }

    [HttpGet("order-item-addons/{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new OrderItemAddonQuery(id));
        return result == null ? NotFound(new { message = "Order item addon not found." }) : Ok(new { message = "Order item addon retrieved successfully.", data = result });
    }

    [NonAction]
    public async Task<IActionResult> Create(Guid orderItemId, CreateOrderItemAddonRequest request)
    {
        var result = await _mediator.Send(new CreateOrderItemAddonCommand(orderItemId, request));
        if (!result.Success)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error ?? "Unable to complete Create for OrderItemAddons." });
            }

            return BadRequest(new { message = result.Error ?? "Unable to complete Create for OrderItemAddons." });
        }

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Data!.Id },
            new { message = "Order item addon created successfully.", data = result.Data });
    }

    [NonAction]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteOrderItemAddonCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Delete for OrderItemAddons." });
        }

        return Ok(new { message = "Order item addon deleted successfully." });
    }

    [NonAction]
    public async Task<IActionResult> Update(Guid id, UpdateOrderItemAddonRequest request)
    {
        var result = await _mediator.Send(new UpdateOrderItemAddonCommand(id, request));
        if (!result.Success)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error ?? "Unable to complete Update for OrderItemAddons." });
            }

            return BadRequest(new { message = result.Error ?? "Unable to complete Update for OrderItemAddons." });
        }

        return Ok(new { message = "Order item addon updated successfully.", data = result.Data });
    }
}
