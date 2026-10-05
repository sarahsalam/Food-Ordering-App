using System.Security.Claims;
using FoodOrdering.Api.Repositories.Interfaces;
using FoodOrdering.Api.Services.Interfaces;

namespace FoodOrdering.Api.Services;

public class MyCouponsService : IMyCouponsService
{
    private readonly ICouponRepository _repository;

    public MyCouponsService(ICouponRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult> GetAsync(ClaimsPrincipal actor)
    {
        var userId = Guid.Parse(actor.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return ServiceResult.Ok(await _repository.GetAssignedAsync(userId));
    }
}
