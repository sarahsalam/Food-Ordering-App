using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodOrdering.Api.Security;

public class ResourceAccessFilter : IAsyncActionFilter
{
    private readonly IResourceAccessService _service;

    public ResourceAccessFilter(IResourceAccessService service)
    {
        _service = service;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;
        Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);

        var request = new ResourceAccessRequest
        {
            Controller = context.RouteData.Values["controller"]?.ToString(),
            UserId = userId,
            IsRead = HttpMethods.IsGet(context.HttpContext.Request.Method),
            Id = GetId(context, "id"),
            CartId = GetId(context, "cartId"),
            OrderId = GetId(context, "orderId"),
            OrderItemId = GetId(context, "orderItemId"),
            RestaurantId = GetId(context, "restaurantId"),
            CategoryId = GetId(context, "categoryId"),
            ProductId = GetId(context, "productId")
        };

        if (request.Controller == "Orders" && request.Id == Guid.Empty)
        {
            context.ActionArguments["userId"] = (Guid?)userId;
        }

        var result = await _service.CheckAsync(request);
        if (result == ResourceAccessResult.NotFound)
        {
            context.Result = new NotFoundResult();
            return;
        }

        if (result == ResourceAccessResult.Forbidden)
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }

    private static Guid GetId(ActionExecutingContext context, string name)
    {
        if (context.ActionArguments.TryGetValue(name, out var value) && value is Guid id)
        {
            return id;
        }

        return Guid.Empty;
    }
}
