using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record AddonsQuery(ProductQueryRequest Query) : IRequest<List<AddonResponse>>;