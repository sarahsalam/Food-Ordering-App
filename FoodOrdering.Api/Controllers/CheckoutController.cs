using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
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
            return Unauthorized(new { message = "Sign in to access checkout." });

        var result = await mediator.Send(new CheckoutCommand(userId, request), cancellationToken);
        if (!result.Success)
        {
            if (result.Error?.StartsWith("CONFLICT:") == true)
                return Conflict(new { message = result.Error[9..] });
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
                return NotFound(new { message = result.Error ?? "Unable to complete Order for Checkout." });
            return BadRequest(new { message = result.Error ?? "Unable to complete Order for Checkout." });
        }

        return CreatedAtAction(nameof(OrdersController.GetById), "Orders", new { id = result.Data!.Id }, new { message = "Checkout completed and order created successfully.", data = result.Data });
    }

}
