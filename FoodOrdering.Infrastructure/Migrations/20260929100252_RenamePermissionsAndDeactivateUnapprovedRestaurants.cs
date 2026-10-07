using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrdering.Api.Migrations
{
    public partial class RenamePermissionsAndDeactivateUnapprovedRestaurants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_approve_restaurants' WHERE \"Name\" = 'Restaurant.Review';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_manage_user_roles' WHERE \"Name\" = 'Users.ManageRoles';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_manage_cart' WHERE \"Name\" = 'Cart.Manage';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_place_orders' WHERE \"Name\" = 'Order.Place';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_view_own_orders' WHERE \"Name\" = 'Order.ViewOwn';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_create_restaurants' WHERE \"Name\" = 'Restaurant.Create';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_manage_own_restaurants' WHERE \"Name\" = 'Restaurant.ManageOwn';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_manage_menu' WHERE \"Name\" = 'Menu.Manage';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_manage_restaurant_orders' WHERE \"Name\" = 'RestaurantOrders.Manage';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_view_assigned_deliveries' WHERE \"Name\" = 'Delivery.ViewAssigned';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_update_assigned_deliveries' WHERE \"Name\" = 'Delivery.UpdateAssigned';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'can_manage_addons' WHERE \"Name\" = 'Addons.Manage';");
            migrationBuilder.Sql("UPDATE \"Restaurants\" SET \"IsActive\" = false WHERE \"ApprovalStatus\" <> 'Approved';");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'Restaurant.Review' WHERE \"Name\" = 'can_approve_restaurants';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'Users.ManageRoles' WHERE \"Name\" = 'can_manage_user_roles';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'Cart.Manage' WHERE \"Name\" = 'can_manage_cart';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'Order.Place' WHERE \"Name\" = 'can_place_orders';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'Order.ViewOwn' WHERE \"Name\" = 'can_view_own_orders';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'Restaurant.Create' WHERE \"Name\" = 'can_create_restaurants';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'Restaurant.ManageOwn' WHERE \"Name\" = 'can_manage_own_restaurants';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'Menu.Manage' WHERE \"Name\" = 'can_manage_menu';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'RestaurantOrders.Manage' WHERE \"Name\" = 'can_manage_restaurant_orders';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'Delivery.ViewAssigned' WHERE \"Name\" = 'can_view_assigned_deliveries';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'Delivery.UpdateAssigned' WHERE \"Name\" = 'can_update_assigned_deliveries';");
            migrationBuilder.Sql("UPDATE \"Permissions\" SET \"Name\" = 'Addons.Manage' WHERE \"Name\" = 'can_manage_addons';");

        }
    }
}
