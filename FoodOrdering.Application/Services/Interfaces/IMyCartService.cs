
namespace FoodOrdering.Application.Services.Interfaces;

public interface IMyCartService
{
    Task<ServiceResult> GetAsync(ClaimsPrincipal actor);
}
