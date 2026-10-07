
namespace FoodOrdering.Infrastructure.Repositories;

public class AddonRepository : IAddonRepository
{
    private readonly AppDbContext _context;

    public AddonRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Addon>> GetAsync(string? search, bool? isActive)
    {
        var query = _context.Addons.AsNoTracking().Where(addon =>
            addon.Restaurant != null && addon.Restaurant.IsActive &&
            addon.Restaurant.ApprovalStatus == RestaurantApprovalStatus.Approved);
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(addon => EF.Functions.ILike(addon.Name, $"%{search.Trim()}%"));
        }

        if (isActive.HasValue)
        {
            query = query.Where(addon => addon.IsActive == isActive);
        }

        return query.ToListAsync();
    }

    public Task<Addon?> GetByIdAsync(Guid id) =>
        _context.Addons.AsNoTracking().FirstOrDefaultAsync(addon => addon.Id == id);

    public Task<Addon?> GetTrackedByIdAsync(Guid id) => _context.Addons.FirstOrDefaultAsync(addon => addon.Id == id);
    public Task AddAsync(Addon addon) => _context.Addons.AddAsync(addon).AsTask();
    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
