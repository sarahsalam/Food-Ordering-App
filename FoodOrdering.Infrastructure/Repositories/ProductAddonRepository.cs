namespace FoodOrdering.Infrastructure.Repositories;

public class ProductAddonRepository : IProductAddonRepository
{
    private readonly AppDbContext _context;

    public ProductAddonRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool>
        ProductExistsAsync(Guid id) => _context.Products.AnyAsync(product => product.Id == id);

    public Task<List<ProductAddon>>
        GetByProductAsync(Guid id) => _context.ProductAddons.AsNoTracking().Include(productAddon => productAddon.Addon)
        .Where(productAddon => productAddon.ProductId == id)
        .ToListAsync();

    public Task<Addon?> GetAddonAsync(Guid id) =>
        _context.Addons.AsNoTracking().FirstOrDefaultAsync(addon => addon.Id == id);

    public Task<Guid?> GetProductRestaurantIdAsync(Guid productId)
    {
        return _context.Products.Where(product => product.Id == productId)
            .Select(product => (Guid?)product.Category.RestaurantId).SingleOrDefaultAsync();
    }

    public Task<bool> ExistsAsync(Guid productId, Guid addonId) => _context.ProductAddons.AnyAsync(productAddon =>
        productAddon.ProductId == productId && productAddon.AddonId == addonId);

    public Task<ProductAddon?> GetTrackedAsync(Guid productId, Guid addonId) =>
        _context.ProductAddons.FirstOrDefaultAsync(productAddon =>
            productAddon.ProductId == productId && productAddon.AddonId == addonId);

    public Task AddAsync(ProductAddon productAddon) => _context.ProductAddons.AddAsync(productAddon).AsTask();
    public void Remove(ProductAddon productAddon) => _context.ProductAddons.Remove(productAddon);
    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
