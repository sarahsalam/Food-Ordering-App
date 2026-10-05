using FoodOrdering.Api.Security;
using FoodOrdering.Api.Services;
using FoodOrdering.Api.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/orders/{orderId:guid}")]
public class OrderTrackingController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderTrackingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("history")]
    public async Task<IActionResult> History(Guid orderId)
    {
        var result = await _mediator.Send(new OrderTrackingHistoryQuery(orderId, User));
        return ToResponse(result);
    }

    [HttpGet("payment")]
    public async Task<IActionResult> Payment(Guid orderId)
    {
        var result = await _mediator.Send(new OrderTrackingPaymentQuery(orderId, User));
        return ToResponse(result);
    }

    [HttpGet("payment/history")]
    public async Task<IActionResult> PaymentHistory(Guid orderId)
    {
        var result = await _mediator.Send(new OrderTrackingPaymentHistoryQuery(orderId, User));
        return ToResponse(result);
    }

    [HttpGet("delivery")]
    public async Task<IActionResult> Delivery(Guid orderId)
    {
        var result = await _mediator.Send(new OrderTrackingDeliveryQuery(orderId, User));
        return ToResponse(result);
    }

    [HttpGet("coupon")]
    public async Task<IActionResult> Coupon(Guid orderId)
    {
        var result = await _mediator.Send(new OrderTrackingCouponQuery(orderId, User));
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
