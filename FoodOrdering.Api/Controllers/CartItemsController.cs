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
public class CartItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("carts/{cartId:guid}/items")]
    public async Task<IActionResult> GetByCart(Guid cartId)
    {
        var result = await _mediator.Send(new CartItemsQuery(cartId));
        return result.Exists ? Ok(result.Data) : NotFound();
    }

    [HttpGet("cart-items/{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new CartItemQuery(id));
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("carts/{cartId:guid}/items")]
    public async Task<IActionResult> Create(Guid cartId, CreateCartItemRequest request)
    {
        var result = await _mediator.Send(new CreateCartItemCommand(cartId, request));
        if (!result.Success)
        {
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

    [HttpPut("cart-items/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCartItemRequest request)
    {
        var result = await _mediator.Send(new UpdateCartItemCommand(id, request));
        if (!result.Success)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        return Ok(result.Data);
    }

    [HttpDelete("cart-items/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCartItemCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(new { message = "Cart item deleted successfully." });
    }
}