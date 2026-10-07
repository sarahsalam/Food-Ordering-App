using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Route("api/orders/{orderId:guid}/payment")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Policy = PermissionNames.PaymentsCollect)]
    [HttpPost("collect")]
    public async Task<IActionResult> Collect(Guid orderId, PaymentActionRequest request)
    {
        var result = await _mediator.Send(new PaymentsCollectCommand(orderId, request, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Order payment collected successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Order payment collected successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order payment: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Order payment: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to collect payment for this order.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [Authorize(Policy = PermissionNames.PaymentsReceive)]
    [HttpPost("submit-to-owner")]
    public async Task<IActionResult> SubmitToOwner(Guid orderId, PaymentActionRequest request)
    {
        var result = await _mediator.Send(new PaymentsSubmitToOwnerCommand(orderId, request, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Payment submitted to restaurant owner successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Payment submitted to restaurant owner successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Payment: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Payment: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to submit this payment to the restaurant owner.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

}
