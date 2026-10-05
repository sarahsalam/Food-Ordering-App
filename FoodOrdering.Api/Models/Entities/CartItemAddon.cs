namespace FoodOrdering.Api.Models.Entities;

public class CartItemAddon
{
    public Guid CartItemId { get; set; }
    public Guid AddonId { get; set; }
    public CartItem CartItem { get; set; } = null!;
    public Addon Addon { get; set; } = null!;
}
