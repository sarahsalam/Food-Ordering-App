using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public class LogoutRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
