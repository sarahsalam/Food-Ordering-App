using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record DeleteCartCommand(Guid Id):IRequest<(bool Success,string? Error)>;
