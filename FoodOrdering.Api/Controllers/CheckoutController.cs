using FoodOrdering.Api.Security;
using FoodOrdering.Api.Mediator.Commands;
using FoodOrdering.Api.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Authorize(Policy = PermissionNames.OrderPlace)]
[Route("api")]
public class CheckoutController(IMediator mediator) : ControllerBase
{
    [HttpPost("checkout")]
    public async Task<IActionResult> Order([FromBody] CheckoutRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Unauthorized();

        var result = await mediator.Send(new CheckoutCommand(userId, request), cancellationToken);
        if (!result.Success)
        {
            if (result.Error?.StartsWith("CONFLICT:") == true)
                return Conflict(new { message = result.Error[9..] });
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
                return NotFound(new { message = result.Error });
            return BadRequest(new { message = result.Error });
        }

        return CreatedAtAction(nameof(OrdersController.GetById), "Orders", new { id = result.Data!.Id }, result.Data);
    }
    
}
