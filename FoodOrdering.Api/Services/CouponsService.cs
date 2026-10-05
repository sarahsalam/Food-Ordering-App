using System.Security.Claims;
using System.Text.RegularExpressions;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;
using FoodOrdering.Api.Security;

namespace FoodOrdering.Api.Services;

public class CouponsService : ICouponsService
{
    private readonly ICouponRepository _repository;

    public CouponsService(ICouponRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult> GetAsync(ClaimsPrincipal actor)
    {
        return ServiceResult.Ok(await _repository.GetAsync());
    }

    public async Task<ServiceResult> CreateAsync(CreateCouponRequest request, ClaimsPrincipal actor)
    {
        var code = request.Code.Trim().ToUpperInvariant();
        if (!Regex.IsMatch(code, "^[A-Z0-9_-]{1,50}$"))
        {
            return ServiceResult.Invalid(new { message = "Use 1–50 letters, digits, underscores, or hyphens for the code." });
        }
        if (request.DiscountType != "Percentage" && request.DiscountType != "Fixed")
        {
            return ServiceResult.Invalid(new { message = "DiscountType must be Percentage or Fixed." });
        }
        if (request.DiscountType == "Percentage" && request.DiscountValue > 100m)
        {
            return ServiceResult.Invalid(new { message = "Percentage discounts cannot exceed 100." });
        }
        if (request.DiscountType == "Fixed" && request.MaximumDiscount.HasValue)
        {
            return ServiceResult.Invalid(new { message = "MaximumDiscount applies only to percentage coupons." });
        }
        if (decimal.Round(request.DiscountValue, 2) != request.DiscountValue ||
            decimal.Round(request.MinimumOrderAmount, 2) != request.MinimumOrderAmount ||
            (request.MaximumDiscount.HasValue && decimal.Round(request.MaximumDiscount.Value, 2) != request.MaximumDiscount.Value))
        {
            return ServiceResult.Invalid(new { message = "Coupon amounts may have at most two decimal places." });
        }
        if (request.StartDate == default || request.ExpiryDate <= request.StartDate || request.ExpiryDate <= DateTimeOffset.UtcNow)
        {
            return ServiceResult.Invalid(new { message = "StartDate is required. ExpiryDate must be in the future and later than StartDate." });
        }
        if (await _repository.CodeExistsAsync(code))
        {
            return ServiceResult.Conflict(new { message = "Coupon code already exists." });
        }
        var coupon = new Coupon
        {
            Id = Guid.NewGuid(), Code = code, Description = request.Description,
            DiscountType = request.DiscountType, DiscountValue = request.DiscountValue,
            MinimumOrderAmount = request.MinimumOrderAmount, MaximumDiscount = request.MaximumDiscount,
            StartDate = request.StartDate.UtcDateTime, ExpiryDate = request.ExpiryDate.UtcDateTime,
            UsageLimit = request.UsageLimit, IsActive = request.IsActive,
            CreatedByAdminId = Guid.Parse(actor.FindFirstValue(ClaimTypes.NameIdentifier)!),
            CreatedAt = DateTime.UtcNow
        };
        _repository.AddCoupon(coupon);
        await _repository.SaveChangesAsync();
        return ServiceResult.Created(new { coupon.Id, coupon.Code });
    }

    public async Task<ServiceResult> SetAvailabilityAsync(Guid id, CouponAvailabilityRequest request, ClaimsPrincipal actor)
    {
        var coupon = await _repository.GetByIdAsync(id);
        if (coupon == null)
        {
            return ServiceResult.Missing();
        }
        coupon.IsActive = request.IsActive;
        await _repository.SaveChangesAsync();
        return ServiceResult.Ok(new { coupon.Id, coupon.Code, coupon.IsActive });
    }

    public async Task<ServiceResult> AssignAsync(Guid couponId, Guid userId, ClaimsPrincipal actor)
    {
        if (!await _repository.ExistsAsync(couponId) ||
            !await _repository.UserExistsAsync(userId))
        {
            return ServiceResult.Missing();
        }
        if (!await _repository.CanReceiveCouponsAsync(userId))
        {
            return ServiceResult.Invalid(new { message = "Assign the can_view_own_coupons permission to this user first." });
        }
        var assignment = await _repository.GetAssignmentAsync(couponId, userId);
        if (assignment != null)
        {
            return ServiceResult.Ok(new { assignment.Id, assignment.CouponId, assignment.UserId, assignment.IsUsed });
        }
        assignment = new UserCoupon
        {
            Id = Guid.NewGuid(), CouponId = couponId, UserId = userId,
            AssignedByAdminId = Guid.Parse(actor.FindFirstValue(ClaimTypes.NameIdentifier)!),
            AssignedAt = DateTime.UtcNow
        };
        _repository.AddUserCoupon(assignment);
        await _repository.SaveChangesAsync();
        return ServiceResult.Created(new { assignment.Id, assignment.CouponId, assignment.UserId, assignment.IsUsed });
    }

    public async Task<ServiceResult> UsagesAsync(Guid couponId, ClaimsPrincipal actor)
    {
        if (!await _repository.ExistsAsync(couponId))
        {
            return ServiceResult.Missing();
        }
        return ServiceResult.Ok((await _repository.GetUsagesAsync(couponId))
            .Select(item => new { item.Id, item.UserId, item.OrderId, item.DiscountAmount, item.UsedAt }).ToList());
    }
}
