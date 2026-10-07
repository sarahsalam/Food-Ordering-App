using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class MyCartGetQueryHandler : IRequestHandler<MyCartGetQuery, ServiceResult>
{
    private readonly IMyCartService _service;

    public MyCartGetQueryHandler(IMyCartService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(MyCartGetQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAsync(request.Actor);
    }
}
