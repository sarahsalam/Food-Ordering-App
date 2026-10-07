using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Application.DTOs.Requests;

public class AssignRolesRequest
{
    [Required]
    [MaxLength(500)]
    public List<string>? Roles { get; set; }
}
