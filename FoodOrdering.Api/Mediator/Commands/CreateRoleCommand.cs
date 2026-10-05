using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record CreateRoleCommand(string Name) : IRequest<ServiceResult>;
