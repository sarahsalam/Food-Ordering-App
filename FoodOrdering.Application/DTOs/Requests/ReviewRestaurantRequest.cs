using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Application.DTOs.Requests;

public class ReviewRestaurantRequest
{
    [MaxLength(2000)]
    public string? Note { get; set; }
}
