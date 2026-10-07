
namespace FoodOrdering.Application.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<Payment?> GetForRiderAsync(Guid orderId, Guid userId);
    Task<Payment?> GetForOwnerAsync(Guid orderId, Guid userId);
    void AddPaymentHistory(PaymentHistory history);
    Task SaveChangesAsync();
}
