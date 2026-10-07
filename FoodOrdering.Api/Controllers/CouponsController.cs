using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Authorize(Policy = PermissionNames.CouponsManage)]
[Route("api/admin/coupons")]
public class CouponsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CouponsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new CouponsGetQuery(User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Coupons retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Coupons retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupons: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupons: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupon not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCouponRequest request)
    {
        var result = await _mediator.Send(new CouponsCreateCommand(request, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Coupon created successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Coupon created successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupon: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupon: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupon not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpPut("{id:guid}/availability")]
    public async Task<IActionResult> SetAvailability(Guid id, CouponAvailabilityRequest request)
    {
        var result = await _mediator.Send(new CouponsSetAvailabilityCommand(id, request, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Coupon availability updated successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Coupon availability updated successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupon availability: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupon availability: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to update this coupon availability.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpPost("{couponId:guid}/users/{userId:guid}")]
    public async Task<IActionResult> Assign(Guid couponId, Guid userId)
    {
        var result = await _mediator.Send(new CouponsAssignCommand(couponId, userId, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Coupon assigned to user successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Coupon assigned to user successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupon: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupon: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Unable to assign this coupon to the user.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

    [HttpGet("{couponId:guid}/usages")]
    public async Task<IActionResult> Usages(Guid couponId)
    {
        var result = await _mediator.Send(new CouponsUsagesQuery(couponId, User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Coupon usages retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Coupon usages retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupon usages: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupon usages: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Coupon not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

}
