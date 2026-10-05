namespace FoodOrdering.Api.Repositories.Interfaces;

public interface IUserAccessRepository
{
    Task<List<string>> GetRolesAsync(Guid userId);
    Task<List<string>> GetPermissionsAsync(Guid userId);
}
