using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class CustomerCatalogGetQueryHandler : IRequestHandler<CustomerCatalogGetQuery, ServiceResult>
{
    private readonly ICustomerCatalogService _service;

    public CustomerCatalogGetQueryHandler(ICustomerCatalogService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(CustomerCatalogGetQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAsync(request.Actor);
    }
}
