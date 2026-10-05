namespace FoodOrdering.Api.Models.Entities;

public class PaymentHistory
{
    public Guid Id { get; set; }
    public Guid PaymentId { get; set; }
    public string? OldStatus { get; set; }
    public string NewStatus { get; set; } = string.Empty;
    public Guid? ChangedByUserId { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public Payment Payment { get; set; } = null!;
    public ApplicationUser? ChangedByUser { get; set; }
}
