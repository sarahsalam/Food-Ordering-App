namespace FoodOrdering.Domain.Entities;

public class UserCoupon
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CouponId { get; set; }
    public Guid AssignedByAdminId { get; set; }
    public DateTime AssignedAt { get; set; }
    public bool IsUsed { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public Coupon Coupon { get; set; } = null!;
    public ApplicationUser AssignedByAdmin { get; set; } = null!;
}
