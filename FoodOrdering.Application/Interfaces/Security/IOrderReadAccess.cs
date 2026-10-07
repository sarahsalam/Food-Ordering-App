
namespace FoodOrdering.Application.Interfaces.Security;

public interface IOrderReadAccess
{
    Task<bool> CanViewAsync(Order order, ClaimsPrincipal user);
}
