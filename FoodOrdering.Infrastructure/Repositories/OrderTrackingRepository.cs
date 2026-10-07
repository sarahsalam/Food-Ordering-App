
namespace FoodOrdering.Infrastructure.Repositories;

public class OrderTrackingRepository : IOrderTrackingRepository
{
    private readonly AppDbContext _context;

    public OrderTrackingRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Order?> GetOrderAsync(Guid orderId)
    {
        return _context.Orders.AsNoTracking().SingleOrDefaultAsync(item => item.Id == orderId);
    }

    public Task<List<OrderStatusHistory>> GetHistoryAsync(Guid orderId)
    {
        return _context.OrderStatusHistories
            .AsNoTracking()
            .Where(item => item.OrderId == orderId)
            .OrderBy(item => item.CreatedAt)
            .ToListAsync();
    }

    public Task<Payment?> GetPaymentAsync(Guid orderId)
    {
        return _context.Payments.AsNoTracking().Include(item => item.Order).SingleOrDefaultAsync(item => item.OrderId == orderId);
    }

    public Task<List<PaymentHistory>> GetPaymentHistoryAsync(Guid orderId)
    {
        return _context.PaymentHistories
            .AsNoTracking()
            .Where(item => item.Payment.OrderId == orderId)
            .OrderBy(item => item.CreatedAt)
            .ToListAsync();
    }

    public Task<Delivery?> GetDeliveryAsync(Guid orderId)
    {
        return _context.Deliveries.AsNoTracking().SingleOrDefaultAsync(item => item.OrderId == orderId);
    }

    public Task<CouponUsage?> GetCouponAsync(Guid orderId)
    {
        return _context.CouponUsages
            .AsNoTracking()
            .Include(item => item.Coupon)
            .SingleOrDefaultAsync(item => item.OrderId == orderId);
    }
}
