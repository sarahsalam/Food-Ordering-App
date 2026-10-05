using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;

namespace FoodOrdering.Api.Services;

public class OrderItemAddonService : IOrderItemAddonService
{
    private readonly IOrderItemAddonRepository _repository;

    public OrderItemAddonService(IOrderItemAddonRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool Exists, List<OrderItemAddonResponse> Data)> GetByOrderItemAsync(Guid orderItemId)
    {
        if (!await _repository.OrderItemExistsAsync(orderItemId))
        {
            return (false, []);
        }

        var items = await _repository.GetByOrderItemAsync(orderItemId);
        return (true, items.Select(Map).ToList());
    }

    public async Task<OrderItemAddonResponse?> GetByIdAsync(Guid id)
    {
        var orderItemAddon = await _repository.GetByIdAsync(id);
        return orderItemAddon == null ? null : Map(orderItemAddon);
    }

    public async Task<(bool Success, string? Error, OrderItemAddonResponse? Data)> CreateAsync(Guid orderItemId,
        CreateOrderItemAddonRequest request)
    {
        if (!await _repository.OrderItemExistsAsync(orderItemId))
        {
            return (false, "Order item was not found.", null);
        }

        if (request.Quantity <= 0)
        {
            return (false, "Quantity must be positive.", null);
        }

        var addon = await _repository.GetAddonAsync(request.AddonId);
        if (addon == null)
        {
            return (false, "Addon was not found.", null);
        }

        var orderItemAddon = new OrderItemAddon
        {
            Id = Guid.NewGuid(),
            OrderItemId = orderItemId,
            AddonId = addon.Id,
            AddonName = addon.Name,
            UnitPrice = addon.Price,
            Quantity = request.Quantity
        };

        await _repository.AddAsync(orderItemAddon);
        await _repository.SaveChangesAsync();
        return (true, null, Map(orderItemAddon));
    }

    public async Task<(bool Success, string? Error, OrderItemAddonResponse? Data)> UpdateAsync(Guid id,
        UpdateOrderItemAddonRequest request)
    {
        var orderItemAddon = await _repository.GetTrackedByIdAsync(id);
        if (orderItemAddon == null)
        {
            return (false, "Order item addon was not found.", null);
        }

        if (request.Quantity <= 0)
        {
            return (false, "Quantity must be positive.", null);
        }

        orderItemAddon.Quantity = request.Quantity;
        await _repository.SaveChangesAsync();
        return (true, null, Map(orderItemAddon));
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(Guid id)
    {
        var orderItemAddon = await _repository.GetTrackedByIdAsync(id);
        if (orderItemAddon == null)
        {
            return (false, "Order item addon was not found.");
        }

        _repository.Remove(orderItemAddon);
        await _repository.SaveChangesAsync();
        return (true, null);
    }

    private static OrderItemAddonResponse Map(OrderItemAddon orderItemAddon) =>
        new(orderItemAddon.Id, orderItemAddon.OrderItemId, orderItemAddon.AddonId, orderItemAddon.AddonName,
            orderItemAddon.UnitPrice, orderItemAddon.Quantity);
}