
namespace FoodOrdering.Application.Interfaces.Repositories;

public interface IRestaurantOperationsRepository
{
    Task<bool> IsOwnerAsync(Guid restaurantId, Guid userId);
    Task<bool> IsStaffAsync(Guid restaurantId, Guid userId);
    Task<List<Order>> GetOrdersAsync(Guid restaurantId);
    Task<Order?> GetOrderAsync(Guid id);
    Task<Delivery?> GetDeliveryAsync(Guid orderId);
    Task<Payment> GetPaymentAsync(Guid orderId);
    void AddDelivery(Delivery delivery);
    void AddOrderStatusHistory(OrderStatusHistory history);
    void AddPaymentHistory(PaymentHistory history);
    Task SaveChangesAsync();
}
