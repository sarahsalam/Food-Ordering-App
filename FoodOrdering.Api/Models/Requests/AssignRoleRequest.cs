using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public class AssignRoleRequest
{
    public Guid RoleId { get; set; }
}
