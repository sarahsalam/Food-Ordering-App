using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Interfaces.Repositories;

public interface ICouponRepository
{
    Task<List<CouponDetailsResponse>> GetAsync();
    Task<List<AssignedCouponResponse>> GetAssignedAsync(Guid userId);
    Task<bool> CodeExistsAsync(string code);
    Task<Coupon?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> UserExistsAsync(Guid id);
    Task<bool> CanReceiveCouponsAsync(Guid userId);
    Task<UserCoupon?> GetAssignmentAsync(Guid couponId, Guid userId);
    Task<List<CouponUsage>> GetUsagesAsync(Guid couponId);
    void AddCoupon(Coupon coupon);
    void AddUserCoupon(UserCoupon assignment);
    Task SaveChangesAsync();
}
