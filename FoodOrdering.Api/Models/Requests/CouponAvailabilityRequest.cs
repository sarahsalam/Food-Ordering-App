using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public class CouponAvailabilityRequest
{
    public bool IsActive { get; set; }
}
