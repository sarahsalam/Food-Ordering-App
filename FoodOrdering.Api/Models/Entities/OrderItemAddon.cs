namespace FoodOrdering.Api.Models.Entities;

public class OrderItemAddon
{
    public Guid Id { get; set; }
    public Guid OrderItemId { get; set; }
    public Guid AddonId { get; set; }
    public string AddonName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public OrderItem OrderItem { get; set; } = null!;
    public Addon Addon { get; set; } = null!;
}
