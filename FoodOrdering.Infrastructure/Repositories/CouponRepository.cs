using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Infrastructure.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly AppDbContext _context;

    public CouponRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<CouponDetailsResponse>> GetAsync()
    {
        return _context.Coupons
            .AsNoTracking()
            .OrderByDescending(item => item.CreatedAt)
            .Select(item => new CouponDetailsResponse
            {
                Id = item.Id,
                Code = item.Code,
                Description = item.Description,
                DiscountType = item.DiscountType,
                DiscountValue = item.DiscountValue,
                MinimumOrderAmount = item.MinimumOrderAmount,
                MaximumDiscount = item.MaximumDiscount,
                StartDate = item.StartDate,
                ExpiryDate = item.ExpiryDate,
                UsageLimit = item.UsageLimit,
                IsActive = item.IsActive,
                CreatedByAdminId = item.CreatedByAdminId,
                CreatedAt = item.CreatedAt,
                UsageCount = _context.CouponUsages.Count(usage => usage.CouponId == item.Id)
            })
            .ToListAsync();
    }

    public Task<List<AssignedCouponResponse>> GetAssignedAsync(Guid userId)
    {
        return _context.UserCoupons
            .AsNoTracking()
            .Where(item => item.UserId == userId)
            .Select(item => new AssignedCouponResponse
            {
                Id = item.Id,
                CouponId = item.CouponId,
                AssignedAt = item.AssignedAt,
                IsUsed = item.IsUsed,
                Code = item.Coupon.Code,
                Description = item.Coupon.Description,
                DiscountType = item.Coupon.DiscountType,
                DiscountValue = item.Coupon.DiscountValue,
                MinimumOrderAmount = item.Coupon.MinimumOrderAmount,
                MaximumDiscount = item.Coupon.MaximumDiscount,
                StartDate = item.Coupon.StartDate,
                ExpiryDate = item.Coupon.ExpiryDate,
                UsageLimit = item.Coupon.UsageLimit,
                IsActive = item.Coupon.IsActive,
                UsageCount = _context.CouponUsages.Count(usage => usage.CouponId == item.CouponId)
            })
            .ToListAsync();
    }

    public Task<bool> CodeExistsAsync(string code)
    {
        return _context.Coupons.AnyAsync(item => item.Code == code);
    }

    public Task<Coupon?> GetByIdAsync(Guid id)
    {
        return _context.Coupons.SingleOrDefaultAsync(item => item.Id == id);
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _context.Coupons.AnyAsync(item => item.Id == id);
    }

    public Task<bool> UserExistsAsync(Guid id)
    {
        return _context.Users.AnyAsync(item => item.Id == id);
    }

    public Task<bool> CanReceiveCouponsAsync(Guid userId)
    {
        return _context.RolePermissions.AnyAsync(item =>
            item.Permission.Name == PermissionNames.CouponsViewOwn &&
            _context.UserRoles.Any(role => role.UserId == userId && role.RoleId == item.RoleId));
    }

    public Task<UserCoupon?> GetAssignmentAsync(Guid couponId, Guid userId)
    {
        return _context.UserCoupons.SingleOrDefaultAsync(item => item.CouponId == couponId && item.UserId == userId);
    }

    public Task<List<CouponUsage>> GetUsagesAsync(Guid couponId)
    {
        return _context.CouponUsages
            .AsNoTracking()
            .Where(item => item.CouponId == couponId)
            .OrderBy(item => item.UsedAt)
            .ToListAsync();
    }

    public void AddCoupon(Coupon coupon)
    {
        _context.Coupons.Add(coupon);
    }

    public void AddUserCoupon(UserCoupon assignment)
    {
        _context.UserCoupons.Add(assignment);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
