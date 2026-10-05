using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Api.Security;
using FoodOrdering.Api.Mediator.Commands;
using FoodOrdering.Api.Mediator.Queries;
using FoodOrdering.Api.Models.Requests;
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
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("carts")]
    public async Task<IActionResult> Create()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return Unauthorized();
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
                return NotFound(new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Data!.Id },
            result.Data);
    }

    [HttpPut("carts/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCartRequest request)
    {
        var result = await _mediator.Send(new UpdateCartCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(result.Data);
    }

    [HttpDelete("carts/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCartCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(new { message = "Cart deleted successfully." });
    }
}
