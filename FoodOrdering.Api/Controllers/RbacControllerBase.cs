using FoodOrdering.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

public abstract class RbacControllerBase : ControllerBase
{
    protected IActionResult ToRbacResponse(ServiceResult result, bool noContent = false) => result.Outcome switch
    {
        ServiceOutcome.Success => noContent ? NoContent() : Ok(result.Data),
        ServiceOutcome.Created => StatusCode(201, result.Data),
        ServiceOutcome.Invalid => BadRequest(result.Data),
        ServiceOutcome.Conflict => Conflict(result.Data),
        ServiceOutcome.NotFound => NotFound(result.Data),
        _ => throw new InvalidOperationException("Unknown service outcome.")
    };
}
