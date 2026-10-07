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
        return result.Exists ? Ok(new { message = "Order items retrieved successfully.", data = result.Data }) : NotFound(new { message = "Order not found." });
    }

    [HttpGet("order-items/{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new OrderItemQuery(id));
        return result == null ? NotFound(new { message = "Order item not found." }) : Ok(new { message = "Order item retrieved successfully.", data = result });
    }

    [NonAction]
    public async Task<IActionResult> Create(Guid orderId, CreateOrderItemRequest request)
    {
        var result = await _mediator.Send(new CreateOrderItemCommand(orderId, request));
        if (!result.Success)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error ?? "Unable to complete Create for OrderItems." });
            }

            return BadRequest(new { message = result.Error ?? "Unable to complete Create for OrderItems." });
        }

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Data!.Id },
            new { message = "Order item created successfully.", data = result.Data });
    }

    [NonAction]
    public async Task<IActionResult> Update(Guid id, UpdateOrderItemRequest request)
    {
        var result = await _mediator.Send(new UpdateOrderItemCommand(id, request));
        if (!result.Success)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error ?? "Unable to complete Update for OrderItems." });
            }

            return BadRequest(new { message = result.Error ?? "Unable to complete Update for OrderItems." });
        }

        return Ok(new { message = "Order item updated successfully.", data = result.Data });
    }
}
