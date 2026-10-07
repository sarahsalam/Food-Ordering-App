namespace FoodOrdering.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RestaurantId { get; set; }
    public Guid BranchId { get; set; }
    public Guid? AssignedRiderId { get; set; }
    public ApplicationUser? AssignedRider { get; set; }
    public Payment? Payment { get; set; }
    public Delivery? Delivery { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public Restaurant Restaurant { get; set; } = null!;
    public RestaurantBranch Branch { get; set; } = null!;
    public ICollection<OrderItem> Items { get; set; } = [];
}
