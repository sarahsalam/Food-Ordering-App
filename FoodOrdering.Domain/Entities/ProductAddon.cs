namespace FoodOrdering.Domain.Entities;

public class ProductAddon
{
    public Guid ProductId { get; set; }
    public Guid AddonId { get; set; }
    public Product Product { get; set; } = null!;
    public Addon Addon { get; set; } = null!;
}
