using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record CreatePermissionCommand(string Name) : IRequest<ServiceResult>;
