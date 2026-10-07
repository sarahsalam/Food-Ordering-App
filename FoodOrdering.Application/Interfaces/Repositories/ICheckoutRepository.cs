
namespace FoodOrdering.Application.Interfaces.Repositories;

public interface ICheckoutRepository
{
    Task<Cart?> GetCartAsync(Guid userId, CancellationToken cancellationToken);
    Task<RestaurantBranch?> GetBranchAsync(Guid branchId, CancellationToken cancellationToken);
    Task<bool> IsAddonLinkedAsync(Guid productId, Guid addonId, CancellationToken cancellationToken);
    Task<UserCoupon?> GetAssignedCouponAsync(Guid userId, string code, CancellationToken cancellationToken);
    Task<int> GetCouponUsageCountAsync(Guid couponId, CancellationToken cancellationToken);
    void AddOrder(Order order);
    void AddOrderHistory(OrderStatusHistory history);
    void AddPayment(Payment payment);
    void AddPaymentHistory(PaymentHistory history);
    void AddCouponUsage(CouponUsage usage);
    void RemoveCartItems(IEnumerable<CartItem> items);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<ICheckoutTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    void ClearTracking();
    bool IsConcurrencyConflict(Exception exception);
}
