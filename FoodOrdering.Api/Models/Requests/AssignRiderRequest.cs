using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public class AssignRiderRequest
{
    public Guid RiderId { get; set; }
}
