using FoodOrdering.Api.Data;
using FoodOrdering.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Api.Security;

public class OrderReadAccess
{
    private readonly AppDbContext _context;
    private readonly UserAccessService _access;

    public OrderReadAccess(AppDbContext context, UserAccessService access)
    {
        _context = context;
        _access = access;
    }

    public async Task<bool> CanViewAsync(Order order, ClaimsPrincipal user)
    {
        if (!Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return false;
        }
        await _access.LoadAsync(userId);
        if (_access.Permissions.Contains(PermissionNames.OrderViewOwn) && order.UserId == userId)
        {
            return true;
        }
        if (_access.Permissions.Contains(PermissionNames.DeliveryViewAssigned) && order.AssignedRiderId == userId)
        {
            return true;
        }
        if (_access.Permissions.Contains(PermissionNames.RestaurantOrdersManage))
        {
            if (await _context.Restaurants.AnyAsync(item => item.Id == order.RestaurantId && item.OwnerId == userId))
            {
                return true;
            }
            if (await _context.RestaurantStaff.AnyAsync(item => item.RestaurantId == order.RestaurantId && item.UserId == userId))
            {
                return true;
            }
        }
        return false;
    }
}
