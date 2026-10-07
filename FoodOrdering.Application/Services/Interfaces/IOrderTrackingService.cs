
namespace FoodOrdering.Application.Services.Interfaces;

public interface IOrderTrackingService
{
    Task<ServiceResult> HistoryAsync(Guid orderId, ClaimsPrincipal actor);
    Task<ServiceResult> PaymentAsync(Guid orderId, ClaimsPrincipal actor);
    Task<ServiceResult> PaymentHistoryAsync(Guid orderId, ClaimsPrincipal actor);
    Task<ServiceResult> DeliveryAsync(Guid orderId, ClaimsPrincipal actor);
    Task<ServiceResult> CouponAsync(Guid orderId, ClaimsPrincipal actor);
}
