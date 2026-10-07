using System.Data;
using Npgsql;

namespace FoodOrdering.Infrastructure.Repositories;

public class CheckoutRepository : ICheckoutRepository
{
    private readonly AppDbContext _context;

    public CheckoutRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Cart?> GetCartAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _context.Carts
            .Include(cart => cart.Items).ThenInclude(item => item.Product).ThenInclude(product => product.Category)
            .Include(cart => cart.Items).ThenInclude(item => item.ProductVariant)
            .Include(cart => cart.Items).ThenInclude(item => item.Addons).ThenInclude(selection => selection.Addon)
            .SingleOrDefaultAsync(cart => cart.UserId == userId, cancellationToken);
    }

    public Task<RestaurantBranch?> GetBranchAsync(Guid branchId, CancellationToken cancellationToken)
    {
        return _context.RestaurantBranches
            .Include(branch => branch.Restaurant)
            .SingleOrDefaultAsync(branch => branch.Id == branchId, cancellationToken);
    }

    public Task<bool> IsAddonLinkedAsync(Guid productId, Guid addonId, CancellationToken cancellationToken)
    {
        return _context.ProductAddons.AnyAsync(link => link.ProductId == productId && link.AddonId == addonId, cancellationToken);
    }

    public Task<UserCoupon?> GetAssignedCouponAsync(Guid userId, string code, CancellationToken cancellationToken)
    {
        return _context.UserCoupons
            .Include(item => item.Coupon)
            .SingleOrDefaultAsync(item => item.UserId == userId && item.Coupon.Code == code, cancellationToken);
    }

    public Task<int> GetCouponUsageCountAsync(Guid couponId, CancellationToken cancellationToken)
    {
        return _context.CouponUsages.CountAsync(usage => usage.CouponId == couponId, cancellationToken);
    }

    public void AddOrder(Order order)
    {
        _context.Orders.Add(order);
    }

    public void AddOrderHistory(OrderStatusHistory history)
    {
        _context.OrderStatusHistories.Add(history);
    }

    public void AddPayment(Payment payment)
    {
        _context.Payments.Add(payment);
    }

    public void AddPaymentHistory(PaymentHistory history)
    {
        _context.PaymentHistories.Add(history);
    }

    public void AddCouponUsage(CouponUsage usage)
    {
        _context.CouponUsages.Add(usage);
    }

    public void RemoveCartItems(IEnumerable<CartItem> items)
    {
        _context.CartItems.RemoveRange(items);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ICheckoutTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
        return new CheckoutTransaction(transaction);
    }

    public void ClearTracking()
    {
        _context.ChangeTracker.Clear();
    }

    public bool IsConcurrencyConflict(Exception exception)
    {
        if (exception is DbUpdateConcurrencyException)
        {
            return true;
        }
        Exception? current = exception;
        while (current != null)
        {
            if (current is PostgresException databaseError)
            {
                return databaseError.SqlState == PostgresErrorCodes.SerializationFailure ||
                       databaseError.SqlState == PostgresErrorCodes.DeadlockDetected;
            }
            current = current.InnerException;
        }
        return false;
    }
}
