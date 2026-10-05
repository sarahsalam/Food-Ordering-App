using FoodOrdering.Api.Models.Entities;

namespace FoodOrdering.Api.Repositories.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<List<ApplicationUser>> GetAllAsync();
    Task<ApplicationUser?> GetByIdAsync(Guid id);
    Task<RefreshToken?> GetRefreshTokenAsync(string tokenHash);
    void AddRefreshToken(RefreshToken token);
    Task SaveRefreshTokensAsync();
}