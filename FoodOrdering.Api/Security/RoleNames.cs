namespace FoodOrdering.Api.Security;

public static class RoleNames
{
    public const string Admin = "Admin";
    public const string Customer = "Customer";
    public const string Staff = "Staff";
    public const string RestaurantOwner = "RestaurantOwner";
    public const string Rider = "Rider";

    public static readonly string[] All = { Admin, Customer, Staff, RestaurantOwner, Rider };
}
