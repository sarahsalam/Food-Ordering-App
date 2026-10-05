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
        return Ok(await _mediator.Send(new OrdersQuery(status, userId)));
    }

    [HttpGet("orders/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new OrderQuery(id));
        return result == null ? NotFound() : Ok(result);
    }

    [NonAction]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var result = await _mediator.Send(new CreateOrderCommand(request));
        if (!result.Success)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            result.Data);
    }

    [NonAction]
    public async Task<IActionResult> Update(Guid id, UpdateOrderRequest request)
    {
        var result = await _mediator.Send(new UpdateOrderCommand(id, request));
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