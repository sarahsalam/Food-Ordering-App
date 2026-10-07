using FoodOrdering.Application.DTOs.Requests;

namespace FoodOrdering.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Category>> GetByRestaurantIdAsync(
        Guid restaurantId,
        CategoryQueryRequest query)
    {
        var categories = _context.Categories
            .Where(category => category.RestaurantId == restaurantId);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = $"%{query.Search.Trim()}%";
            categories = categories.Where(category =>
                EF.Functions.ILike(category.Name, search));
        }

        if (query.IsActive.HasValue)
        {
            categories = categories.Where(category =>
                category.IsActive == query.IsActive.Value);
        }

        return categories.ToListAsync();
    }

    public Task<Category?> GetByIdAsync(Guid id) =>
        _context.Categories.FirstOrDefaultAsync(category => category.Id == id);

    public Task<bool> RestaurantExistsAsync(Guid restaurantId) =>
        _context.Restaurants.AnyAsync(restaurant => restaurant.Id == restaurantId);

    public Task AddAsync(Category category) =>
        _context.Categories.AddAsync(category).AsTask();

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
