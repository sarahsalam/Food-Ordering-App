using FoodOrdering.Application.Mediator.Queries;
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
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Order history retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Order history retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order history: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order history: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order history not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpGet("payment")]
    public async Task<IActionResult> Payment(Guid orderId)
    {
        var result = await _mediator.Send(new OrderTrackingPaymentQuery(orderId, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Order payment retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Order payment retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order payment: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order payment: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order payment not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpGet("payment/history")]
    public async Task<IActionResult> PaymentHistory(Guid orderId)
    {
        var result = await _mediator.Send(new OrderTrackingPaymentHistoryQuery(orderId, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Payment history retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Payment history retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Payment history: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Payment history: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order payment history not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpGet("delivery")]
    public async Task<IActionResult> Delivery(Guid orderId)
    {
        var result = await _mediator.Send(new OrderTrackingDeliveryQuery(orderId, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Order delivery retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Order delivery retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order delivery: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order delivery: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order delivery not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpGet("coupon")]
    public async Task<IActionResult> Coupon(Guid orderId)
    {
        var result = await _mediator.Send(new OrderTrackingCouponQuery(orderId, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Order coupon retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Order coupon retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order coupon: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order coupon: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order coupon not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

}
