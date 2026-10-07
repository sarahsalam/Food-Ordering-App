namespace FoodOrdering.Domain.Entities;

public class Addon
{
    public Guid Id { get; set; }
    public Guid? RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<ProductAddon> ProductAddons { get; set; } = [];
    public ICollection<OrderItemAddon> OrderItemAddons { get; set; } = [];
}
