
namespace FoodOrdering.Application.Services.Interfaces;

public interface IMyCouponsService
{
    Task<ServiceResult> GetAsync(ClaimsPrincipal actor);
}
