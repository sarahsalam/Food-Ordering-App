using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Api.Security;
using FoodOrdering.Api.Mediator.Commands;
using FoodOrdering.Api.Mediator.Queries;
using FoodOrdering.Api.Models.Requests;
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
        return result.Exists ? Ok(result.Data) : NotFound();
    }

    [HttpGet("order-item-addons/{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new OrderItemAddonQuery(id));
        return result == null ? NotFound() : Ok(result);
    }

    [NonAction]
    public async Task<IActionResult> Create(Guid orderItemId, CreateOrderItemAddonRequest request)
    {
        var result = await _mediator.Send(new CreateOrderItemAddonCommand(orderItemId, request));
        if (!result.Success)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Data!.Id },
            result.Data);
    }

    [NonAction]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteOrderItemAddonCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
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
                return NotFound(new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        return Ok(result.Data);
    }
}