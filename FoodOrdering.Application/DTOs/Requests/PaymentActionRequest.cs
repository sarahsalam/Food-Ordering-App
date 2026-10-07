using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Application.DTOs.Requests;

public class PaymentActionRequest
{
    [MaxLength(2000)]
    public string? Note { get; set; }
}
