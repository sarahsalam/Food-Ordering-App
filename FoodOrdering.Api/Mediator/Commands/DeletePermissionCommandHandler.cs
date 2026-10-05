using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, ServiceResult>
{
    private readonly RbacService _service;

    public DeletePermissionCommandHandler(RbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        return _service.DeletePermissionAsync(request.PermissionId);
    }
}
