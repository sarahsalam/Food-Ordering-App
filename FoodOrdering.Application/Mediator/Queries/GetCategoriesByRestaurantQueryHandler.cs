using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class GetCategoriesByRestaurantQueryHandler
    : IRequestHandler<GetCategoriesByRestaurantQuery, (bool Exists, List<CategoryResponse> Data)>
{
    private readonly ICategoryService _service;

    public GetCategoriesByRestaurantQueryHandler(ICategoryService service)
    {
        _service = service;
    }

    public Task<(bool Exists, List<CategoryResponse> Data)> Handle(
        GetCategoriesByRestaurantQuery request,
        CancellationToken cancellationToken) =>
        _service.GetByRestaurantIdAsync(request.RestaurantId, request.Query);
}
