using FoodOrdering.Api.Security;
using FoodOrdering.Api.Services;
using FoodOrdering.Api.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Authorize(Policy = PermissionNames.RestaurantView)]
[Route("api/catalog/restaurants")]
public class CustomerCatalogController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerCatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new CustomerCatalogGetQuery(User));
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
