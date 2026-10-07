namespace FoodOrdering.Application.DTOs.Responses;

public record CartResponse(Guid Id, Guid UserId, DateTime CreatedAt, DateTime? UpdatedAt);
