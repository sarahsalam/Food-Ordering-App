namespace FoodOrdering.Domain.Entities;

public class Cart
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public ICollection<CartItem> Items { get; set; } = [];
}
