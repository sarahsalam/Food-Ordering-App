using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services;

public class MyCartService : IMyCartService
{
    private readonly ICartRepository _repository;

    public MyCartService(ICartRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult> GetAsync(ClaimsPrincipal actor)
    {
        var userId = Guid.Parse(actor.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var cart = await _repository.GetForUserAsync(userId);
        if (cart == null)
        {
            return ServiceResult.Missing(new { message = "Create a cart first using POST /api/carts." });
        }
        return ServiceResult.Ok(new CartResponse(cart.Id, cart.UserId, cart.CreatedAt, cart.UpdatedAt));
    }
}
