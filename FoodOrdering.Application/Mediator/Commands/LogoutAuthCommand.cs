using FoodOrdering.Application.DTOs.Requests;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record LogoutAuthCommand(LogoutRequest Request) : IRequest;
