using FoodOrdering.Api.Data;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Cart?> GetByIdAsync(Guid id) =>
        _context.Carts.AsNoTracking().FirstOrDefaultAsync(cart => cart.Id == id);

    public Task<Cart?> GetTrackedByIdAsync(Guid id) => _context.Carts.FirstOrDefaultAsync(cart => cart.Id == id);
    public Task<bool> UserExistsAsync(Guid id) => _context.Users.AnyAsync(user => user.Id == id);
    public Task<bool> ExistsForUserAsync(Guid id) => _context.Carts.AnyAsync(cart => cart.UserId == id);
    public Task AddAsync(Cart cart) => _context.Carts.AddAsync(cart).AsTask();
    public void Remove(Cart cart) => _context.Carts.Remove(cart);
    public Task SaveChangesAsync() => _context.SaveChangesAsync();
    public Task<Cart?> GetForUserAsync(Guid userId)
    {
        return _context.Carts.AsNoTracking().SingleOrDefaultAsync(item => item.UserId == userId);
    }
}