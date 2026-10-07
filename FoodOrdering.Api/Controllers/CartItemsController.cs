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
        return result.Exists ? Ok(new { message = "Cart items retrieved successfully.", data = result.Data }) : NotFound(new { message = "Cart not found." });
    }

    [HttpGet("cart-items/{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new CartItemQuery(id));
        return result == null ? NotFound(new { message = "Cart item not found." }) : Ok(new { message = "Cart item retrieved successfully.", data = result });
    }

    [HttpPost("carts/{cartId:guid}/items")]
    public async Task<IActionResult> Create(Guid cartId, CreateCartItemRequest request)
    {
        var result = await _mediator.Send(new CreateCartItemCommand(cartId, request));
        if (!result.Success)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error ?? "Unable to complete Create for CartItems." });
            }

            return BadRequest(new { message = result.Error ?? "Unable to complete Create for CartItems." });
        }

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Data!.Id },
            new { message = "Cart item created successfully.", data = result.Data });
    }

    [HttpPut("cart-items/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCartItemRequest request)
    {
        var result = await _mediator.Send(new UpdateCartItemCommand(id, request));
        if (!result.Success)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(new { message = result.Error ?? "Unable to complete Update for CartItems." });
            }

            return BadRequest(new { message = result.Error ?? "Unable to complete Update for CartItems." });
        }

        return Ok(new { message = "Cart item updated successfully.", data = result.Data });
    }

    [HttpDelete("cart-items/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCartItemCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Delete for CartItems." });
        }

        return Ok(new { message = "Cart item deleted successfully." });
    }
}
