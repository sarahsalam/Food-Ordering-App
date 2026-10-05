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
        return ToResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCouponRequest request)
    {
        var result = await _mediator.Send(new CouponsCreateCommand(request, User));
        return ToResponse(result);
    }

    [HttpPut("{id:guid}/availability")]
    public async Task<IActionResult> SetAvailability(Guid id, CouponAvailabilityRequest request)
    {
        var result = await _mediator.Send(new CouponsSetAvailabilityCommand(id, request, User));
        return ToResponse(result);
    }

    [HttpPost("{couponId:guid}/users/{userId:guid}")]
    public async Task<IActionResult> Assign(Guid couponId, Guid userId)
    {
        var result = await _mediator.Send(new CouponsAssignCommand(couponId, userId, User));
        return ToResponse(result);
    }

    [HttpGet("{couponId:guid}/usages")]
    public async Task<IActionResult> Usages(Guid couponId)
    {
        var result = await _mediator.Send(new CouponsUsagesQuery(couponId, User));
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
