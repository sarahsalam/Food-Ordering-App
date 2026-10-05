using FoodOrdering.Api.Models.Entities;

namespace FoodOrdering.Api.Repositories.Interfaces;

public interface IOrderTrackingRepository
{
    Task<Order?> GetOrderAsync(Guid orderId);
    Task<List<OrderStatusHistory>> GetHistoryAsync(Guid orderId);
    Task<Payment?> GetPaymentAsync(Guid orderId);
    Task<List<PaymentHistory>> GetPaymentHistoryAsync(Guid orderId);
    Task<Delivery?> GetDeliveryAsync(Guid orderId);
    Task<CouponUsage?> GetCouponAsync(Guid orderId);
}
