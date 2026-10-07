namespace FoodOrdering.Domain.Entities;

public class Delivery
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid RiderId { get; set; }
    public Guid? AssignedByUserId { get; set; }
    public DateTime? AssignedAt { get; set; }
    public DateTime? PickedUpAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public Order Order { get; set; } = null!;
    public ApplicationUser Rider { get; set; } = null!;
    public ApplicationUser? AssignedByUser { get; set; }
}
