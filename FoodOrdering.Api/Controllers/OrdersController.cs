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
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("orders")]
    public async Task<IActionResult> Get([FromQuery] string? status, [FromQuery] Guid? userId)
    {
        return Ok(new { message = "Orders retrieved successfully.", data = await _mediator.Send(new OrdersQuery(status, userId)) });
    }

    [HttpGet("orders/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new OrderQuery(id));
        return result == null ? NotFound(new { message = "Order not found." }) : Ok(new { message = "Order retrieved successfully.", data = result });
    }

    [NonAction]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var result = await _mediator.Send(new CreateOrderCommand(request));
        if (!result.Success)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error ?? "Unable to complete Create for Orders." });
            }

            return BadRequest(new { message = result.Error ?? "Unable to complete Create for Orders." });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            new { message = "Order created successfully.", data = result.Data });
    }

    [NonAction]
    public async Task<IActionResult> Update(Guid id, UpdateOrderRequest request)
    {
        var result = await _mediator.Send(new UpdateOrderCommand(id, request));
        if (!result.Success)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error ?? "Unable to complete Update for Orders." });
            }

            return BadRequest(new { message = result.Error ?? "Unable to complete Update for Orders." });
        }

        return Ok(new { message = "Order updated successfully.", data = result.Data });
    }
}
