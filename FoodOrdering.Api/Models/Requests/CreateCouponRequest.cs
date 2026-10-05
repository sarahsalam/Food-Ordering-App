using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public class CreateCouponRequest
{
    [Required, MaxLength(50)]
    public string Code { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string? Description { get; set; }
    [Required]
    public string DiscountType { get; set; } = string.Empty;
    [Range(typeof(decimal), "0.01", "9999999999999999.99")]
    public decimal DiscountValue { get; set; }
    [Range(typeof(decimal), "0", "9999999999999999.99")]
    public decimal MinimumOrderAmount { get; set; }
    [Range(typeof(decimal), "0.01", "9999999999999999.99")]
    public decimal? MaximumDiscount { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
    [Range(1, int.MaxValue)]
    public int? UsageLimit { get; set; }
    public bool IsActive { get; set; } = true;
}
