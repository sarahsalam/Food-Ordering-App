using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Security;
using FoodOrdering.Api.Services;
using FoodOrdering.Api.Mediator.Commands;
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
        return ToResponse(result);
    }

    [Authorize(Policy = PermissionNames.PaymentsReceive)]
    [HttpPost("submit-to-owner")]
    public async Task<IActionResult> SubmitToOwner(Guid orderId, PaymentActionRequest request)
    {
        var result = await _mediator.Send(new PaymentsSubmitToOwnerCommand(orderId, request, User));
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
