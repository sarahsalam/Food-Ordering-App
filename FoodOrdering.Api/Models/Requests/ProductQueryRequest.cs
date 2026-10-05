using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Api.Models.Requests;

public record ProductQueryRequest(string? Search,bool? IsActive);
