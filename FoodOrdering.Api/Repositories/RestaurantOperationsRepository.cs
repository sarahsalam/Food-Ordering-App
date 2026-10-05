using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Data;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class RestaurantOperationsRepository : IRestaurantOperationsRepository
{
    private readonly AppDbContext _context;

    public RestaurantOperationsRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> IsOwnerAsync(Guid restaurantId, Guid userId)
    {
        return _context.Restaurants.AnyAsync(item => item.Id == restaurantId && item.OwnerId == userId);
    }

    public Task<bool> IsStaffAsync(Guid restaurantId, Guid userId)
    {
        return _context.RestaurantStaff.AnyAsync(item => item.RestaurantId == restaurantId && item.UserId == userId);
    }

    public Task<List<Order>> GetOrdersAsync(Guid restaurantId)
    {
        return _context.Orders
            .AsNoTracking()
            .Include(item => item.Items)
            .ThenInclude(item => item.Addons)
            .Where(item => item.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public Task<Order?> GetOrderAsync(Guid id)
    {
        return _context.Orders.SingleOrDefaultAsync(item => item.Id == id);
    }

    public Task<Delivery?> GetDeliveryAsync(Guid orderId)
    {
        return _context.Deliveries.SingleOrDefaultAsync(item => item.OrderId == orderId);
    }

    public Task<Payment> GetPaymentAsync(Guid orderId)
    {
        return _context.Payments.SingleAsync(item => item.OrderId == orderId);
    }

    public void AddDelivery(Delivery delivery)
    {
        _context.Deliveries.Add(delivery);
    }

    public void AddOrderStatusHistory(OrderStatusHistory history)
    {
        _context.OrderStatusHistories.Add(history);
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
