using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public class ReviewRestaurantRequest
{
    [MaxLength(2000)]
    public string? Note { get; set; }
}
