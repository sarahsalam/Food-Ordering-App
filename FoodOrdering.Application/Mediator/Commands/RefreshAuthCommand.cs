using FoodOrdering.Application.DTOs.Requests;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record RefreshAuthCommand(RefreshTokenRequest Request) : IRequest<(bool Success, string? Error, string? AccessToken, string? RefreshToken)>;
