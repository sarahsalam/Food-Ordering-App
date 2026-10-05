using FoodOrdering.Api.Models.Entities;

namespace FoodOrdering.Api.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAsync(string? status, Guid? userId);
    Task<Order?> GetByIdAsync(Guid id);
    Task<Order?> GetTrackedByIdAsync(Guid id);
    Task<(bool User, bool Restaurant, bool Branch, bool BranchBelongs)> ValidateReferencesAsync(Guid userId, Guid restaurantId, Guid branchId);
    Task AddAsync(Order order);
    Task SaveChangesAsync();
}