using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;

namespace FoodOrdering.Api.Services;

public class DeliveriesService : IDeliveriesService
{
    private readonly IDeliveryRepository _repository;

    public DeliveriesService(IDeliveryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult> GetAsync(ClaimsPrincipal actor)
    {
        var userId = Guid.Parse(actor.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var orders = (await _repository.GetAssignedAsync(userId))
            .Select(item => new
            {
                Id = item.OrderId, DeliveryId = item.Id, item.Order.Status, item.Order.BranchId,
                item.Order.DeliveryAddress, item.AssignedAt, item.PickedUpAt, item.DeliveredAt,
                Amount = item.Order.Payment!.Amount, PaymentStatus = item.Order.Payment.Status
            }).ToList();
        return ServiceResult.Ok(orders);
    }

    public async Task<ServiceResult> UpdateStatusAsync(Guid id, ChangeOrderStatusRequest request, ClaimsPrincipal actor)
    {
        var userId = Guid.Parse(actor.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var delivery = await _repository.GetAssignedOrderAsync(id, userId);
        if (delivery == null)
        {
            return ServiceResult.Missing();
        }
        var order = delivery.Order;
        bool allowed = false;
        if (order.Status == "ReadyForPickup" && request.Status == "OutForDelivery")
        {
            allowed = true;
        }
        if (order.Status == "OutForDelivery" && request.Status == "Delivered")
        {
            allowed = true;
        }
        if (!allowed)
        {
            return ServiceResult.Invalid(new { message = "That delivery status transition is not allowed." });
        }
        var now = DateTime.UtcNow;
        if (request.Status == "OutForDelivery")
        {
            delivery.PickedUpAt = now;
        }
        if (request.Status == "Delivered")
        {
            delivery.DeliveredAt = now;
        }
        _repository.AddOrderStatusHistory(new OrderStatusHistory
        {
            Id = Guid.NewGuid(), OrderId = order.Id, OldStatus = order.Status, NewStatus = request.Status,
            ChangedByUserId = userId, Note = request.Note, CreatedAt = now
        });
        order.Status = request.Status;
        order.UpdatedAt = now;
        await _repository.SaveChangesAsync();
        return ServiceResult.Ok(new { order.Id, order.Status });
    }
}
