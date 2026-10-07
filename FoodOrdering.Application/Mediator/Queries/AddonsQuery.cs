using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record AddonsQuery(ProductQueryRequest Query) : IRequest<List<AddonResponse>>;
