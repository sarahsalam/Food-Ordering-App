namespace FoodOrdering.Application.DTOs.Responses;

public record PermissionsResponse(List<string> Roles, List<string> Permissions);
