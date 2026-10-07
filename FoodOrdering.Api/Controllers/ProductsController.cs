using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Route("api")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("categories/{categoryId:guid}/products")]
    public async Task<IActionResult> GetByCategory(Guid categoryId, [FromQuery] string? search,
        [FromQuery] bool? isActive)
    {
        var result = await _mediator.Send(new ProductsQuery(categoryId, new(search, isActive)));
        return result.Exists ? Ok(new { message = "Products retrieved successfully.", data = result.Data }) : NotFound(new { message = "Category not found." });
    }

    [HttpGet("products/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new ProductQuery(id));
        return result == null ? NotFound(new { message = "Product not found." }) : Ok(new { message = "Product retrieved successfully.", data = result });
    }

    [HttpPost("categories/{categoryId:guid}/products")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Create(Guid categoryId, CreateProductRequest request)
    {
        var result = await _mediator.Send(new CreateProductCommand(categoryId, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Create for Products." });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            new { message = "Product created successfully.", data = result.Data });
    }

    [HttpPut("products/{id:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequest request)
    {
        var result = await _mediator.Send(new UpdateProductCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Update for Products." });
        }

        return Ok(new { message = "Product updated successfully.", data = result.Data });
    }

    [HttpDelete("products/{id:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Delete for Products." });
        }

        return Ok(new { message = "Product deleted successfully." });
    }
}
