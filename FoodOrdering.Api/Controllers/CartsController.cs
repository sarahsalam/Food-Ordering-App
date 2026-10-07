using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[Authorize(Policy = PermissionNames.CartManage)]
[ApiController]
[Route("api")]
public class CartsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("carts/{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new CartQuery(id));
        return result == null ? NotFound(new { message = "Cart not found." }) : Ok(new { message = "Cart retrieved successfully.", data = result });
    }

    [HttpPost("carts")]
    public async Task<IActionResult> Create()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized(new { message = "Sign in to access carts." });
        }

        var request = new CreateCartRequest(userId);
        var result = await _mediator.Send(new CreateCartCommand(request));
        if (!result.Success)
        {
            if (result.Error?.StartsWith("CONFLICT:") == true)
            {
                return Conflict(new { message = result.Error[9..] });
            }

            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error ?? "Unable to complete Create for Carts." });
            }

            return BadRequest(new { message = result.Error ?? "Unable to complete Create for Carts." });
        }

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Data!.Id },
            new { message = "Cart created successfully.", data = result.Data });
    }

    [HttpPut("carts/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCartRequest request)
    {
        var result = await _mediator.Send(new UpdateCartCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Update for Carts." });
        }

        return Ok(new { message = "Cart updated successfully.", data = result.Data });
    }

    [HttpDelete("carts/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCartCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Delete for Carts." });
        }

        return Ok(new { message = "Cart deleted successfully." });
    }
}
