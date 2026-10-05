namespace FoodOrdering.Api.Models.Entities;

public class OrderStatusHistory
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string? OldStatus { get; set; }
    public string NewStatus { get; set; } = string.Empty;
    public Guid? ChangedByUserId { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public Order Order { get; set; } = null!;
    public ApplicationUser? ChangedByUser { get; set; }
}
