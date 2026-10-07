
namespace FoodOrdering.Application.Interfaces.Repositories;

public interface IDeliveryRepository
{
    Task<List<Delivery>> GetAssignedAsync(Guid userId);
    Task<Delivery?> GetAssignedOrderAsync(Guid id, Guid userId);
    void AddOrderStatusHistory(OrderStatusHistory history);
    Task SaveChangesAsync();
}
