using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record DeleteProductAddonCommand(Guid ProductId,Guid AddonId):IRequest<(bool Success,string? Error)>;
