namespace FoodOrdering.Application.DTOs.Requests;

public class ResourceAccessRequest
{
    public string? Controller { get; set; }
    public Guid UserId { get; set; }
    public bool IsRead { get; set; }
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public Guid OrderId { get; set; }
    public Guid OrderItemId { get; set; }
    public Guid RestaurantId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ProductId { get; set; }
}
