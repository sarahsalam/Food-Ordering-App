using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ServiceResult>
{
    private readonly RbacService _service;

    public CreateRoleCommandHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.CreateRoleAsync(request.Name);
    }
}
