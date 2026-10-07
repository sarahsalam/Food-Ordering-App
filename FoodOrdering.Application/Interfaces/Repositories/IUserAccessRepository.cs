namespace FoodOrdering.Application.Interfaces.Repositories;

public interface IUserAccessRepository
{
    Task<List<string>> GetRolesAsync(Guid userId);
    Task<List<string>> GetPermissionsAsync(Guid userId);
}
