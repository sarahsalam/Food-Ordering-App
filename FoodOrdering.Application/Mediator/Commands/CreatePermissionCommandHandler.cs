using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, ServiceResult>
{
    private readonly IRbacService _service;

    public CreatePermissionCommandHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        return _service.CreatePermissionAsync(request.Name);
    }
}
