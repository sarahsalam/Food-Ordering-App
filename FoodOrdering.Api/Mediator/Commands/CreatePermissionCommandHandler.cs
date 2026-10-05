using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, ServiceResult>
{
    private readonly RbacService _service;

    public CreatePermissionCommandHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        return _service.CreatePermissionAsync(request.Name);
    }
}
