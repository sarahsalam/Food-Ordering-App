using System.Security.Claims;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;
using FoodOrdering.Api.Security;

namespace FoodOrdering.Api.Services;

public class RestaurantOperationsService : IRestaurantOperationsService
{
    private readonly IRestaurantOperationsRepository _repository;
    private readonly UserManager<ApplicationUser> _users;
    private readonly IUserAccessRepository _accessRepository;

    public RestaurantOperationsService(IRestaurantOperationsRepository repository, UserManager<ApplicationUser> users,
        IUserAccessRepository accessRepository)
    {
        _repository = repository;
        _users = users;
        _accessRepository = accessRepository;
    }

    public async Task<ServiceResult> OrdersAsync(Guid restaurantId, ClaimsPrincipal actor)
    {
        if (!await CanManageAsync(restaurantId, actor))
        {
            return ServiceResult.Missing();
        }
        var orders = (await _repository.GetOrdersAsync(restaurantId))
            .Select(item => new
            {
                item.Id, item.Status, item.TotalAmount, item.DeliveryAddress, item.AssignedRiderId,
                Items = item.Items.Select(line => new
                {
                    line.ProductName,
                    line.VariantName,
                    line.Quantity,
                    Addons = line.Addons.Select(addon => new { addon.AddonName, addon.Quantity })
                })
            }).ToList();
        return ServiceResult.Ok(orders);
    }

    public async Task<ServiceResult> UpdateStatusAsync(Guid id, ChangeOrderStatusRequest request, ClaimsPrincipal actor)
    {
        var order = await _repository.GetOrderAsync(id);
        if (order == null || !await CanManageAsync(order.RestaurantId, actor))
        {
            return ServiceResult.Missing();
        }
        bool allowed = false;
        if (order.Status == "Pending" && request.Status == "Accepted")
        {
            allowed = true;
        }
        if (order.Status == "Accepted" && request.Status == "Preparing")
        {
            allowed = true;
        }
        if (order.Status == "Preparing" && request.Status == "ReadyForPickup")
        {
            allowed = true;
        }
        if (order.Status == "Pending" && request.Status == "Cancelled")
        {
            allowed = true;
        }
        if (!allowed)
        {
            return ServiceResult.Invalid(new { message = "That order status transition is not allowed." });
        }
        _repository.AddOrderStatusHistory(new OrderStatusHistory
        {
            Id = Guid.NewGuid(), OrderId = order.Id, OldStatus = order.Status, NewStatus = request.Status,
            ChangedByUserId = Guid.Parse(actor.FindFirstValue(ClaimTypes.NameIdentifier)!),
            Note = request.Note, CreatedAt = DateTime.UtcNow
        });
        order.Status = request.Status;
        order.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync();
        return ServiceResult.Ok(new { order.Id, order.Status });
    }

    public async Task<ServiceResult> AssignRiderAsync(Guid id, AssignRiderRequest request, ClaimsPrincipal actor)
    {
        var order = await _repository.GetOrderAsync(id);
        if (order == null || !await CanManageAsync(order.RestaurantId, actor))
        {
            return ServiceResult.Missing();
        }
        if (order.Status != "Accepted" && order.Status != "Preparing" && order.Status != "ReadyForPickup")
        {
            return ServiceResult.Invalid(new { message = "Assign a rider after acceptance and before pickup." });
        }
        var rider = await _users.FindByIdAsync(request.RiderId.ToString());
        if (rider == null || !(await _accessRepository.GetPermissionsAsync(rider.Id))
                .Contains(PermissionNames.DeliveryUpdateAssigned))
        {
            return ServiceResult.Invalid(new { message = "Select a user with the can_update_assigned_deliveries permission." });
        }
        var now = DateTime.UtcNow;
        var delivery = await _repository.GetDeliveryAsync(order.Id);
        if (delivery == null)
        {
            delivery = new Delivery { Id = Guid.NewGuid(), OrderId = order.Id };
            _repository.AddDelivery(delivery);
        }
        delivery.RiderId = rider.Id;
        delivery.AssignedByUserId = Guid.Parse(actor.FindFirstValue(ClaimTypes.NameIdentifier)!);
        delivery.AssignedAt = now;
        order.AssignedRiderId = rider.Id;
        order.UpdatedAt = now;
        var payment = await _repository.GetPaymentAsync(order.Id);
        payment.RiderId = rider.Id;
        payment.UpdatedAt = now;
        _repository.AddPaymentHistory(new PaymentHistory
        {
            Id = Guid.NewGuid(), PaymentId = payment.Id, OldStatus = payment.Status, NewStatus = payment.Status,
            ChangedByUserId = delivery.AssignedByUserId,
            Note = $"Delivery assigned to rider {rider.Id}.", CreatedAt = now
        });
        await _repository.SaveChangesAsync();
        return ServiceResult.Ok(new { order.Id, order.AssignedRiderId });
    }

    private async Task<bool> CanManageAsync(Guid restaurantId, ClaimsPrincipal actor)
    {
        if (!Guid.TryParse(actor.FindFirstValue(ClaimTypes.NameIdentifier), out var userId) ||
            !(await _accessRepository.GetPermissionsAsync(userId)).Contains(PermissionNames.RestaurantOrdersManage))
        {
            return false;
        }
        return await _repository.IsOwnerAsync(restaurantId, userId) ||
               await _repository.IsStaffAsync(restaurantId, userId);
    }
}
