using Microsoft.AspNetCore.Authorization;
using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.Mediator.Commands;
using FoodOrdering.Application.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Api.Controllers;

[ApiController]
[Route("api")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("restaurants/{restaurantId:guid}/categories")]
    public async Task<IActionResult> GetByRestaurant(
        Guid restaurantId,
        [FromQuery] CategoryQueryRequest query)
    {
        var result = await _mediator.Send(
            new GetCategoriesByRestaurantQuery(restaurantId, query));
        return result.Exists ? Ok(new { message = "Categories retrieved successfully.", data = result.Data }) : NotFound(new { message = "Restaurant not found." });
    }

    [HttpGet("categories/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));
        return category == null ? NotFound(new { message = "Category not found." }) : Ok(new { message = "Category retrieved successfully.", data = category });
    }

    [HttpPost("restaurants/{restaurantId:guid}/categories")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Create(
        Guid restaurantId,
        CreateCategoryRequest request)
    {
        var result = await _mediator.Send(
            new CreateCategoryCommand(restaurantId, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Create for Categories." });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            new { message = "Category created successfully.", data = result.Data });
    }

    [HttpPut("categories/{id:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryRequest request)
    {
        var result = await _mediator.Send(new UpdateCategoryCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Update for Categories." });
        }

        return Ok(new { message = "Category updated successfully.", data = result.Data });
    }

    [HttpDelete("categories/{id:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error ?? "Unable to complete Delete for Categories." });
        }

        return Ok(new { message = "Category deleted successfully." });

    }

}
