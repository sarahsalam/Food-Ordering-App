using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Security;
using FoodOrdering.Api.Services;
using FoodOrdering.Api.Mediator.Commands;
using FoodOrdering.Api.Mediator.Queries;
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
        return ToResponse(result);
    }

    [HttpPut("orders/{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, ChangeOrderStatusRequest request)
    {
        var result = await _mediator.Send(new RestaurantOperationsUpdateStatusCommand(id, request, User));
        return ToResponse(result);
    }

    [HttpPut("orders/{id:guid}/rider")]
    public async Task<IActionResult> AssignRider(Guid id, AssignRiderRequest request)
    {
        var result = await _mediator.Send(new RestaurantOperationsAssignRiderCommand(id, request, User));
        return ToResponse(result);
    }

    private IActionResult ToResponse(ServiceResult result)
    {
        switch (result.Outcome)
        {
            case ServiceOutcome.Success:
                return Ok(result.Data);
            case ServiceOutcome.Created:
                return StatusCode(201, result.Data);
            case ServiceOutcome.Invalid:
                return BadRequest(result.Data);
            case ServiceOutcome.Conflict:
                return Conflict(result.Data);
            case ServiceOutcome.NotFound:
                return result.Data == null ? NotFound() : NotFound(result.Data);
            default:
                throw new InvalidOperationException("Unknown service outcome.");
        }
    }
}
