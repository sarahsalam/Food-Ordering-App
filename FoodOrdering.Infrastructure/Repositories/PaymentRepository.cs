
namespace FoodOrdering.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Payment?> GetForRiderAsync(Guid orderId, Guid userId)
    {
        return _context.Payments
            .Include(item => item.Order)
            .SingleOrDefaultAsync(item => item.OrderId == orderId && item.RiderId == userId);
    }

    public Task<Payment?> GetForOwnerAsync(Guid orderId, Guid userId)
    {
        return _context.Payments
            .Include(item => item.Order)
            .SingleOrDefaultAsync(item => item.OrderId == orderId && item.Order.Restaurant.OwnerId == userId);
    }

    public void AddPaymentHistory(PaymentHistory history)
    {
        _context.PaymentHistories.Add(history);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
