using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public class CreateRbacNameRequest
{
    [Required]
    [StringLength(100)]
    [RegularExpression(@"^[A-Za-z][A-Za-z0-9_.-]*$")]
    public string Name { get; set; } = string.Empty;
}
