using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Api.Security;
using FoodOrdering.Api.Mediator.Commands;
using FoodOrdering.Api.Mediator.Queries;
using FoodOrdering.Api.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Route("api")]
public class ProductVariantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductVariantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("products/{productId:guid}/variants")]
    public async Task<IActionResult> GetByProduct(Guid productId, [FromQuery] string? search,
        [FromQuery] bool? isActive)
    {
        var result = await _mediator.Send(new VariantsQuery(productId, new(search, isActive)));
        return result.Exists ? Ok(result.Data) : NotFound();
    }

    [HttpGet("product-variants/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new ProductVariantQuery(id));
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("products/{productId:guid}/variants")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Create(Guid productId, CreateProductVariantRequest request)
    {
        var result = await _mediator.Send(new CreateProductVariantCommand(productId, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            result.Data);
    }

    [HttpPut("product-variants/{id:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Update(Guid id, UpdateProductVariantRequest request)
    {
        var result = await _mediator.Send(new UpdateProductVariantCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(result.Data);
    }

    [HttpDelete("product-variants/{id:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductVariantCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(new { message = "Product variant deleted successfully." });
    }
}