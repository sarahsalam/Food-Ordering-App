namespace FoodOrdering.Domain.Entities;

public class CouponUsage
{
    public Guid Id { get; set; }
    public Guid CouponId { get; set; }
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime UsedAt { get; set; }
    public Coupon Coupon { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public Order Order { get; set; } = null!;
}
