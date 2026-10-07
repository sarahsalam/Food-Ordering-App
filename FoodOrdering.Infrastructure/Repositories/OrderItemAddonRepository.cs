
namespace FoodOrdering.Infrastructure.Repositories;

public class OrderItemAddonRepository : IOrderItemAddonRepository
{
    private readonly AppDbContext _context;

    public OrderItemAddonRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> OrderItemExistsAsync(Guid id) => _context.OrderItems.AnyAsync(orderItem => orderItem.Id == id);

    public Task<List<OrderItemAddon>> GetByOrderItemAsync(Guid id) =>
        _context.OrderItemAddons.AsNoTracking().Where(orderItemAddon => orderItemAddon.OrderItemId == id).ToListAsync();

    public Task<OrderItemAddon?> GetByIdAsync(Guid id) =>
        _context.OrderItemAddons.AsNoTracking().FirstOrDefaultAsync(orderItemAddon => orderItemAddon.Id == id);

    public Task<OrderItemAddon?> GetTrackedByIdAsync(Guid id) =>
        _context.OrderItemAddons.FirstOrDefaultAsync(orderItemAddon => orderItemAddon.Id == id);

    public Task<Addon?> GetAddonAsync(Guid id) =>
        _context.Addons.AsNoTracking().FirstOrDefaultAsync(addon => addon.Id == id);

    public Task AddAsync(OrderItemAddon orderItemAddon) => _context.OrderItemAddons.AddAsync(orderItemAddon).AsTask();
    public Task SaveChangesAsync() => _context.SaveChangesAsync();

    public void Remove(OrderItemAddon orderItemAddon) => _context.OrderItemAddons.Remove(orderItemAddon);
}
