using FoodOrdering.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Authorize(Policy = PermissionNames.CouponsViewOwn)]
[Route("api/coupons/mine")]
public class MyCouponsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MyCouponsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new MyCouponsGetQuery(User));
        return result.Outcome switch
        {
            ServiceOutcome.Success => Ok(new { message = "Assigned coupons retrieved successfully.", data = result.Data }),
            ServiceOutcome.Created => StatusCode(201, new { message = "Assigned coupons retrieved successfully.", data = result.Data }),
            ServiceOutcome.Invalid => BadRequest(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Assigned coupons: The input is invalid.", data = result.Data }),
            ServiceOutcome.Conflict => Conflict(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Assigned coupons: The current state prevents this operation.", data = result.Data }),
            ServiceOutcome.NotFound => NotFound(new { message = result.Data?.GetType().GetProperty("message")?.GetValue(result.Data) as string ?? "Assigned coupon not found.", data = result.Data }),
            _ => throw new InvalidOperationException("Unknown service outcome.")
        };
    }

}
