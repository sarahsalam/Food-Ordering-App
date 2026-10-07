using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Authorize(Policy = PermissionNames.RestaurantOrdersManage)]
[Route("api/restaurant-operations")]
public class RestaurantOperationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantOperationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("restaurants/{restaurantId:guid}/orders")]
    public async Task<IActionResult> Orders(Guid restaurantId)
    {
        var result = await _mediator.Send(new RestaurantOperationsOrdersQuery(restaurantId, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Restaurant orders retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Restaurant orders retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurant orders: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurant orders: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Restaurant not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpPut("orders/{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, ChangeOrderStatusRequest request)
    {
        var result = await _mediator.Send(new RestaurantOperationsUpdateStatusCommand(id, request, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Order status updated successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Order status updated successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order status: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order status: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to update this order status.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpPut("orders/{id:guid}/rider")]
    public async Task<IActionResult> AssignRider(Guid id, AssignRiderRequest request)
    {
        var result = await _mediator.Send(new RestaurantOperationsAssignRiderCommand(id, request, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Rider assigned to order successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Rider assigned to order successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Rider: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Rider: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to assign a rider to this order.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

}
