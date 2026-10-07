namespace FoodOrdering.Domain.Constants;

public static class RolePermissions
{
    public static readonly Dictionary<string, string[]> Defaults = new()
    {
        [RoleNames.Admin] = new[]
        {
            PermissionNames.CouponsManage, PermissionNames.RestaurantView, PermissionNames.RestaurantReview,
            PermissionNames.UsersManageRoles
        },
        [RoleNames.Customer] = new[]
        {
            PermissionNames.CouponsViewOwn, PermissionNames.RestaurantView, PermissionNames.CartManage,
            PermissionNames.OrderPlace, PermissionNames.OrderViewOwn
        },
        [RoleNames.RestaurantOwner] = new[]
        {
            PermissionNames.AddonsManage, PermissionNames.PaymentsReceive, PermissionNames.RestaurantView,
            PermissionNames.RestaurantCreate, PermissionNames.RestaurantManageOwn, PermissionNames.MenuManage,
            PermissionNames.RestaurantOrdersManage
        },
        [RoleNames.Staff] = new[] { PermissionNames.RestaurantView, PermissionNames.RestaurantOrdersManage },
        [RoleNames.Rider] = new[]
        {
            PermissionNames.PaymentsCollect, PermissionNames.RestaurantView, PermissionNames.DeliveryViewAssigned,
            PermissionNames.DeliveryUpdateAssigned
        }
    };
}
