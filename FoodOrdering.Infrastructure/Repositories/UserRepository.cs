
namespace FoodOrdering.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(user => user.Restaurants)
            .ThenInclude(restaurant => restaurant.Branches)
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public Task<List<ApplicationUser>> GetAllAsync() =>
        _context.Users
            .Include(user => user.Restaurants)
            .ThenInclude(restaurant => restaurant.Branches)
            .ToListAsync();

    public Task<ApplicationUser?> GetByIdAsync(Guid id) =>
        _context.Users
            .Include(user => user.Restaurants)
            .ThenInclude(restaurant => restaurant.Branches)
            .FirstOrDefaultAsync(user => user.Id == id);
    public Task<RefreshToken?> GetRefreshTokenAsync(string tokenHash)
    {
        return _context.RefreshTokens.Include(token => token.User).SingleOrDefaultAsync(token => token.Token == tokenHash);
    }

    public void AddRefreshToken(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);
    }

    public async Task SaveRefreshTokensAsync()
    {
        await _context.SaveChangesAsync();
    }
}
