using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _repository;

    public OrderItemService(IOrderItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool Exists, List<OrderItemResponse> Data)> GetByOrderAsync(Guid orderId)
    {
        if (!await _repository.OrderExistsAsync(orderId))
        {
            return (false, []);
        }

        var items = await _repository.GetByOrderAsync(orderId);
        return (true, items.Select(Map).ToList());
    }

    public async Task<OrderItemResponse?> GetByIdAsync(Guid id)
    {
        var orderItem = await _repository.GetByIdAsync(id);
        return orderItem == null ? null : Map(orderItem);
    }

    public async Task<(bool Success, string? Error, OrderItemResponse? Data)> CreateAsync(Guid orderId,
        CreateOrderItemRequest request)
    {
        if (!await _repository.OrderExistsAsync(orderId))
        {
            return (false, "Order was not found.", null);
        }

        if (request.Quantity <= 0)
        {
            return (false, "Quantity must be positive.", null);
        }

        var (product, variant) = await _repository.GetPricingAsync(request.ProductId, request.ProductVariantId);
        if (product == null)
        {
            return (false, "Product was not found.", null);
        }

        if (request.ProductVariantId.HasValue && variant == null)
        {
            return (false, "Variant does not belong to product.", null);
        }

        decimal price = product.BasePrice;
        if (variant != null)
        {
            price = variant.Price;
        }
        var orderItem = new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            ProductId = product.Id,
            ProductVariantId = variant?.Id,
            ProductName = product.Name,
            VariantName = variant?.Name,
            UnitPrice = price,
            Quantity = request.Quantity,
            TotalPrice = price * request.Quantity
        };

        await _repository.AddAsync(orderItem);
        await _repository.SaveChangesAsync();
        return (true, null, Map(orderItem));
    }

    public async Task<(bool Success, string? Error, OrderItemResponse? Data)> UpdateAsync(Guid id,
        UpdateOrderItemRequest request)
    {
        var orderItem = await _repository.GetTrackedByIdAsync(id);
        if (orderItem == null)
        {
            return (false, "Order item was not found.", null);
        }

        if (request.Quantity <= 0)
        {
            return (false, "Quantity must be positive.", null);
        }

        orderItem.Quantity = request.Quantity;
        orderItem.TotalPrice = orderItem.UnitPrice * request.Quantity;
        await _repository.SaveChangesAsync();
        return (true, null, Map(orderItem));
    }

    private static OrderItemResponse Map(OrderItem orderItem) => new(orderItem.Id, orderItem.OrderId,
        orderItem.ProductId, orderItem.ProductVariantId, orderItem.ProductName,
        orderItem.VariantName, orderItem.UnitPrice, orderItem.Quantity, orderItem.TotalPrice);
}
