
namespace FoodOrdering.Application.Services;

public class OrderTrackingService : IOrderTrackingService
{
    private readonly IOrderTrackingRepository _repository;
    private readonly IOrderReadAccess _access;

    public OrderTrackingService(IOrderTrackingRepository repository, IOrderReadAccess access)
    {
        _repository = repository;
        _access = access;
    }

    public async Task<ServiceResult> HistoryAsync(Guid orderId, ClaimsPrincipal actor)
    {
        var order = await _repository.GetOrderAsync(orderId);
        if (order == null || !await _access.CanViewAsync(order, actor))
        {
            return ServiceResult.Missing();
        }
        return ServiceResult.Ok((await _repository.GetHistoryAsync(orderId))
            .Select(item => new { item.Id, item.OldStatus, item.NewStatus, item.ChangedByUserId, item.Note, item.CreatedAt }).ToList());
    }

    public async Task<ServiceResult> PaymentAsync(Guid orderId, ClaimsPrincipal actor)
    {
        var order = await _repository.GetOrderAsync(orderId);
        if (order == null || !await _access.CanViewAsync(order, actor))
        {
            return ServiceResult.Missing();
        }
        var payment = await _repository.GetPaymentAsync(orderId);
        return payment == null ? ServiceResult.Missing() : ServiceResult.Ok(new
            {
                payment.Id, payment.OrderId, payment.Amount, payment.PaymentMethod, payment.Status, payment.RiderId,
                payment.CollectedAt, payment.SubmittedToOwnerAt, payment.CreatedAt, payment.UpdatedAt,
                OrderStatus = payment.Order.Status
            });
    }

    public async Task<ServiceResult> PaymentHistoryAsync(Guid orderId, ClaimsPrincipal actor)
    {
        var order = await _repository.GetOrderAsync(orderId);
        if (order == null || !await _access.CanViewAsync(order, actor))
        {
            return ServiceResult.Missing();
        }
        return ServiceResult.Ok((await _repository.GetPaymentHistoryAsync(orderId))
            .Select(item => new { item.Id, item.OldStatus, item.NewStatus, item.ChangedByUserId, item.Note, item.CreatedAt }).ToList());
    }

    public async Task<ServiceResult> DeliveryAsync(Guid orderId, ClaimsPrincipal actor)
    {
        var order = await _repository.GetOrderAsync(orderId);
        if (order == null || !await _access.CanViewAsync(order, actor))
        {
            return ServiceResult.Missing();
        }
        var delivery = await _repository.GetDeliveryAsync(orderId);
        return delivery == null ? ServiceResult.Missing() : ServiceResult.Ok(new { delivery.Id, delivery.OrderId, delivery.RiderId, delivery.AssignedByUserId, delivery.AssignedAt, delivery.PickedUpAt, delivery.DeliveredAt });
    }

    public async Task<ServiceResult> CouponAsync(Guid orderId, ClaimsPrincipal actor)
    {
        var order = await _repository.GetOrderAsync(orderId);
        if (order == null || !await _access.CanViewAsync(order, actor))
        {
            return ServiceResult.Missing();
        }
        var usage = await _repository.GetCouponAsync(orderId);
        return usage == null ? ServiceResult.Missing() : ServiceResult.Ok(new { usage.CouponId, usage.Coupon.Code, usage.DiscountAmount, usage.UsedAt });
    }
}
