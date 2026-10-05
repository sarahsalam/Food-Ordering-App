using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record LoginAuthCommand(LoginRequest Request) : IRequest<(bool Success, string? Error, string? AccessToken, string? RefreshToken, UserResponse? User)>;
