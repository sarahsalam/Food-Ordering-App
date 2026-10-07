using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, ServiceResult>
{
    private readonly IRbacService _service;

    public DeletePermissionCommandHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        return _service.DeletePermissionAsync(request.PermissionId);
    }
}
