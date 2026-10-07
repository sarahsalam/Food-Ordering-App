using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
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
        return result.Exists ? Ok(new { message = "Product variants retrieved successfully.", data = result.Data }) : NotFound(new { message = "Product not found." });
    }

    [HttpGet("product-variants/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new ProductVariantQuery(id));
        return result == null ? NotFound(new { message = "Product variant not found." }) : Ok(new { message = "Product variant retrieved successfully.", data = result });
    }

    [HttpPost("products/{productId:guid}/variants")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Create(Guid productId, CreateProductVariantRequest request)
    {
        var result = await _mediator.Send(new CreateProductVariantCommand(productId, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Create for ProductVariants." });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            new { message = "Product variant created successfully.", data = result.Data });
    }

    [HttpPut("product-variants/{id:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Update(Guid id, UpdateProductVariantRequest request)
    {
        var result = await _mediator.Send(new UpdateProductVariantCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Update for ProductVariants." });
        }

        return Ok(new { message = "Product variant updated successfully.", data = result.Data });
    }

    [HttpDelete("product-variants/{id:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductVariantCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Delete for ProductVariants." });
        }

        return Ok(new { message = "Product variant deleted successfully." });
    }
}
