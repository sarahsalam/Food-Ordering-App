using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ServiceResult>
{
    private readonly IRbacService _service;

    public CreateRoleCommandHandler(IRbacService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.CreateRoleAsync(request.Name);
    }
}
