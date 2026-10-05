using FoodOrdering.Api.Models.Entities;

namespace FoodOrdering.Api.Repositories.Interfaces;

public interface IPaymentRepository
{
    Task<Payment?> GetForRiderAsync(Guid orderId, Guid userId);
    Task<Payment?> GetForOwnerAsync(Guid orderId, Guid userId);
    void AddPaymentHistory(PaymentHistory history);
    Task SaveChangesAsync();
}
