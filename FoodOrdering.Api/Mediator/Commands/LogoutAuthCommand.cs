using FoodOrdering.Api.Models.Requests;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record LogoutAuthCommand(LogoutRequest Request) : IRequest;
