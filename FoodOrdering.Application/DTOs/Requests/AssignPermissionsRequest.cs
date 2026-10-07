using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Application.DTOs.Requests;

public class AssignPermissionsRequest
{
    [Required]
    [MaxLength(500)]
    public List<string>? Permissions { get; set; }
}
