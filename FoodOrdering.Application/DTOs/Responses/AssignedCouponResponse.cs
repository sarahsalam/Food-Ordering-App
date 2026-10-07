namespace FoodOrdering.Application.DTOs.Responses;

public class AssignedCouponResponse
{
    public Guid Id { get; set; }
    public Guid CouponId { get; set; }
    public DateTime AssignedAt { get; set; }
    public bool IsUsed { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string DiscountType { get; set; } = string.Empty;
    public decimal DiscountValue { get; set; }
    public decimal MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int? UsageLimit { get; set; }
    public bool IsActive { get; set; }
    public int UsageCount { get; set; }
}
