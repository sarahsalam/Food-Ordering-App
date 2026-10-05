using FoodOrdering.Api.Models.Entities;

namespace FoodOrdering.Api.Repositories.Interfaces;

public interface IAdminRepository
{
    Task<IdentityRole<Guid>?> GetRoleAsync(Guid id);
    Task<List<IdentityRole<Guid>>> GetRolesAsync();
    Task<bool> UserExistsAsync(Guid userId);
    Task<List<IdentityRole<Guid>>> GetUserRolesAsync(Guid userId);
    Task<bool> RestaurantExistsAsync(Guid restaurantId);
    Task<bool> IsStaffAssignedAsync(Guid restaurantId, Guid userId);
    Task<List<Restaurant>> GetRestaurantsAsync(RestaurantApprovalStatus approvalStatus);
    Task<Restaurant?> GetRestaurantAsync(Guid id);
    void AddRestaurantStaff(RestaurantStaff assignment);
    Task SaveChangesAsync();
}
