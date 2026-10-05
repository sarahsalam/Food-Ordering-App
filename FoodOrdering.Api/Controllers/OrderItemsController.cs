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
public class OrderItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("orders/{orderId:guid}/items")]
    public async Task<IActionResult> GetByOrder(Guid orderId)
    {
        var result = await _mediator.Send(new OrderItemsQuery(orderId));
        return result.Exists ? Ok(result.Data) : NotFound();
    }

    [HttpGet("order-items/{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new OrderItemQuery(id));
        return result == null ? NotFound() : Ok(result);
    }

    [NonAction]
    public async Task<IActionResult> Create(Guid orderId, CreateOrderItemRequest request)
    {
        var result = await _mediator.Send(new CreateOrderItemCommand(orderId, request));
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
    public async Task<IActionResult> Update(Guid id, UpdateOrderItemRequest request)
    {
        var result = await _mediator.Send(new UpdateOrderItemCommand(id, request));
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