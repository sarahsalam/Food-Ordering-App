namespace FoodOrdering.Application.DTOs.Requests;

public record ProductQueryRequest(string? Search,bool? IsActive);
