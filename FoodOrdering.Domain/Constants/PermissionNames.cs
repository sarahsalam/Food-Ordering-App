namespace FoodOrdering.Domain.Constants;
public static class PermissionNames
{
    public const string CouponsManage = "can_manage_coupons";
    public const string CouponsViewOwn = "can_view_own_coupons";
    public const string PaymentsCollect = "can_collect_cod";
    public const string PaymentsReceive = "can_receive_cod";
    public const string RestaurantView = "can_view_restaurants";
    public const string RestaurantReview = "can_approve_restaurants";
    public const string UsersManageRoles = "can_manage_user_roles";
    public const string CartManage = "can_manage_cart";
    public const string OrderPlace = "can_place_orders";
    public const string OrderViewOwn = "can_view_own_orders";
    public const string RestaurantCreate = "can_create_restaurants";
    public const string RestaurantManageOwn = "can_manage_own_restaurants";
    public const string MenuManage = "can_manage_menu";
    public const string RestaurantOrdersManage = "can_manage_restaurant_orders";
    public const string DeliveryViewAssigned = "can_view_assigned_deliveries";
    public const string DeliveryUpdateAssigned = "can_update_assigned_deliveries";
    public const string AddonsManage = "can_manage_addons";

    public static readonly string[] All =
    {
        CouponsManage,
        CouponsViewOwn,
        PaymentsCollect,
        PaymentsReceive,
        RestaurantView,
        RestaurantReview,
        UsersManageRoles,
        CartManage,
        OrderPlace,
        OrderViewOwn,
        RestaurantCreate,
        RestaurantManageOwn,
        MenuManage,
        RestaurantOrdersManage,
        DeliveryViewAssigned,
        DeliveryUpdateAssigned,
        AddonsManage
    };
}
