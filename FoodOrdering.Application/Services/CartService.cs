using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;

    public CartService(ICartRepository repository)
    {
        _repository = repository;
    }

    public async Task<CartResponse?> GetByIdAsync(Guid id)
    {
        var cart = await _repository.GetByIdAsync(id);
        return cart == null ? null : Map(cart);
    }

    public async Task<(bool Success, string? Error, CartResponse? Data)> CreateAsync(CreateCartRequest request)
    {
        if (!await _repository.UserExistsAsync(request.UserId))
        {
            return (false, "User was not found.", null);
        }

        if (await _repository.ExistsForUserAsync(request.UserId))
        {
            return (false, "CONFLICT:User already has a cart. Please use the existing cart.", null);
        }

        var cart = new Cart
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(cart);
        await _repository.SaveChangesAsync();
        return (true, null, Map(cart));
    }

    public async Task<(bool Success, string? Error, CartResponse? Data)> UpdateAsync(Guid id, UpdateCartRequest request)
    {
        var cart = await _repository.GetTrackedByIdAsync(id);
        if (cart == null)
        {
            return (false, "Cart was not found.", null);
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null, Map(cart));
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(Guid id)
    {
        var cart = await _repository.GetTrackedByIdAsync(id);
        if (cart == null)
        {
            return (false, "Cart was not found.");
        }

        _repository.Remove(cart);
        await _repository.SaveChangesAsync();
        return (true, null);
    }

    private static CartResponse Map(Cart cart) => new(cart.Id, cart.UserId, cart.CreatedAt, cart.UpdatedAt);
}
