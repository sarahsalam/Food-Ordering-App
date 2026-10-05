using Microsoft.AspNetCore.Authorization;

namespace FoodOrdering.Api.Security;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly UserAccessService _access;

    public PermissionHandler(UserAccessService access)
    {
        _access = access;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (!Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return;
        }
        await _access.LoadAsync(userId);
        if (_access.Permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
