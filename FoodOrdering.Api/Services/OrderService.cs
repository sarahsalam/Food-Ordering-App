using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;

namespace FoodOrdering.Api.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<OrderResponse>> GetAsync(string? status, Guid? userId) =>
        (await _repository.GetAsync(status, userId)).Select(Map).ToList();

    public async Task<OrderResponse?> GetByIdAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        return order == null ? null : Map(order);
    }

    public async Task<(bool Success, string? Error, OrderResponse? Data)> CreateAsync(CreateOrderRequest request)
    {
        var references =
            await _repository.ValidateReferencesAsync(request.UserId, request.RestaurantId, request.BranchId);
        if (!references.User)
        {
            return (false, "User was not found.", null);
        }

        if (!references.Restaurant || !references.Branch)
        {
            return (false, "Restaurant or branch was not found.", null);
        }

        if (!references.BranchBelongs)
        {
            return (false, "Branch does not belong to restaurant.", null);
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            RestaurantId = request.RestaurantId,
            BranchId = request.BranchId,
            Status = request.Status,
            Subtotal = request.Subtotal,
            Tax = request.Tax,
            DeliveryFee = request.DeliveryFee,
            Discount = request.Discount,
            TotalAmount = request.TotalAmount,
            DeliveryAddress = request.DeliveryAddress,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(order);
        await _repository.SaveChangesAsync();
        return (true, null, Map(order));
    }

    public async Task<(bool Success, string? Error, OrderResponse? Data)> UpdateAsync(Guid id,
        UpdateOrderRequest request)
    {
        var order = await _repository.GetTrackedByIdAsync(id);
        if (order == null)
        {
            return (false, "Order was not found.", null);
        }

        order.Status = request.Status;
        order.Subtotal = request.Subtotal;
        order.Tax = request.Tax;
        order.DeliveryFee = request.DeliveryFee;
        order.Discount = request.Discount;
        order.TotalAmount = request.TotalAmount;
        order.DeliveryAddress = request.DeliveryAddress;
        order.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return (true, null, Map(order));
    }

    private static OrderResponse Map(Order order) => new(order.Id, order.UserId, order.RestaurantId, order.BranchId,
        order.Status, order.Subtotal, order.Tax,
        order.DeliveryFee, order.Discount, order.TotalAmount, order.DeliveryAddress, order.CreatedAt, order.UpdatedAt);
}