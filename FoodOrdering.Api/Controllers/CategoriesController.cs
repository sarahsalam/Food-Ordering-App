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
        return result.Exists ? Ok(result.Data) : NotFound();
    }

    [HttpGet("categories/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));
        return category == null ? NotFound() : Ok(category);
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
            return NotFound(new { message = result.Error });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            result.Data);
    }

    [HttpPut("categories/{id:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryRequest request)
    {
        var result = await _mediator.Send(new UpdateCategoryCommand(id, request));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(result.Data);
    }

    [HttpDelete("categories/{id:guid}")]
    [Authorize(Policy = PermissionNames.MenuManage)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(id));
        if (!result.Success)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(new { message = "Category deleted successfully." });

    }
    
}