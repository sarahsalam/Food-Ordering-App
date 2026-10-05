using FoodOrdering.Api.Repositories.Interfaces;

namespace FoodOrdering.Api.Security;

public class UserAccessService
{
    private readonly IUserAccessRepository _repository;
    private Guid? _loadedUserId;
    public List<string> Roles { get; private set; } = new();
    public List<string> Permissions { get; private set; } = new();

    public UserAccessService(IUserAccessRepository repository)
    {
        _repository = repository;
    }

    public async Task LoadAsync(Guid userId)
    {
        if (_loadedUserId == userId)
        {
            return;
        }
        Roles = await _repository.GetRolesAsync(userId);
        Permissions = await _repository.GetPermissionsAsync(userId);
        Permissions.Sort(StringComparer.Ordinal);
        _loadedUserId = userId;
    }
}
