
namespace FoodOrdering.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CategoryExistsAsync(Guid id) => _context.Categories.AnyAsync(category => category.Id == id);

    public Task<List<Product>> GetByCategoryAsync(Guid id, string? search, bool? isActive)
    {
        var query = _context.Products.AsNoTracking().Where(product => product.CategoryId == id);
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(product => EF.Functions.ILike(product.Name, $"%{search.Trim()}%"));
        }

        if (isActive.HasValue)
        {
            query = query.Where(product => product.IsActive == isActive);
        }

        return query.ToListAsync();
    }

    public Task<Product?> GetByIdAsync(Guid id) =>
        _context.Products.AsNoTracking().FirstOrDefaultAsync(product => product.Id == id);

    public Task<Product?> GetTrackedByIdAsync(Guid id) =>
        _context.Products.FirstOrDefaultAsync(product => product.Id == id);

    public Task AddAsync(Product product) => _context.Products.AddAsync(product).AsTask();
    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
