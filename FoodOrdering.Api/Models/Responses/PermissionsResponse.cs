namespace FoodOrdering.Api.Models.Responses;

public record PermissionsResponse(List<string> Roles, List<string> Permissions);
