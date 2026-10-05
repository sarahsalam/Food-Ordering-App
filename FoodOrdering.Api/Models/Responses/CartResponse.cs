namespace FoodOrdering.Api.Models.Responses;

public record CartResponse(Guid Id, Guid UserId, DateTime CreatedAt, DateTime? UpdatedAt);