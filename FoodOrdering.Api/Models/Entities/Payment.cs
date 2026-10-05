namespace FoodOrdering.Api.Models.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = "CashOnDelivery";
    public string Status { get; set; } = "Pending";
    public Guid? RiderId { get; set; }
    public DateTime? CollectedAt { get; set; }
    public DateTime? SubmittedToOwnerAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Order Order { get; set; } = null!;
    public ApplicationUser? Rider { get; set; }
}
