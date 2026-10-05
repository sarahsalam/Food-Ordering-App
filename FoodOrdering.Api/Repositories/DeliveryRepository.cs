using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Data;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly AppDbContext _context;

    public DeliveryRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Delivery>> GetAssignedAsync(Guid userId)
    {
        return _context.Deliveries
            .AsNoTracking()
            .Include(item => item.Order)
            .ThenInclude(item => item.Payment)
            .Where(item => item.RiderId == userId)
            .ToListAsync();
    }

    public Task<Delivery?> GetAssignedOrderAsync(Guid id, Guid userId)
    {
        return _context.Deliveries
            .Include(item => item.Order)
            .SingleOrDefaultAsync(item => item.OrderId == id && item.RiderId == userId);
    }

    public void AddOrderStatusHistory(OrderStatusHistory history)
    {
        _context.OrderStatusHistories.Add(history);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
