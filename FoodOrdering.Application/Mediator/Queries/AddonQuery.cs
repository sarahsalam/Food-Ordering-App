using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record AddonQuery(Guid Id):IRequest<AddonResponse?>;
