using FoodOrdering.Application.DTOs.Requests;

namespace FoodOrdering.Application.Services;

public class PaymentsService : IPaymentsService
{
    private readonly IPaymentRepository _repository;

    public PaymentsService(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult> CollectAsync(Guid orderId, PaymentActionRequest request, ClaimsPrincipal actor)
    {
        var userId = Guid.Parse(actor.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var payment = await _repository.GetForRiderAsync(orderId, userId);
        if (payment == null || payment.Order.AssignedRiderId != userId)
        {
            return ServiceResult.Missing();
        }
        if (payment.Order.Status != "Delivered" || payment.Status != "Pending")
        {
            return ServiceResult.Conflict(new { message = "Cash can be collected once, after the order is delivered." });
        }
        var now = DateTime.UtcNow;
        _repository.AddPaymentHistory(new PaymentHistory
        {
            Id = Guid.NewGuid(), PaymentId = payment.Id, OldStatus = payment.Status, NewStatus = "Collected",
            ChangedByUserId = userId, Note = request.Note, CreatedAt = now
        });
        payment.Status = "Collected";
        payment.CollectedAt = now;
        payment.UpdatedAt = now;
        await _repository.SaveChangesAsync();
        return ServiceResult.Ok(new { payment.Id, payment.OrderId, payment.Amount, payment.Status, payment.CollectedAt });
    }

    public async Task<ServiceResult> SubmitToOwnerAsync(Guid orderId, PaymentActionRequest request, ClaimsPrincipal actor)
    {
        var userId = Guid.Parse(actor.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var payment = await _repository.GetForOwnerAsync(orderId, userId);
        if (payment == null)
        {
            return ServiceResult.Missing();
        }
        if (payment.Order.Status != "Delivered" || payment.Status != "Collected")
        {
            return ServiceResult.Conflict(new { message = "The owner can confirm receipt only after the rider has collected cash." });
        }
        var now = DateTime.UtcNow;
        _repository.AddPaymentHistory(new PaymentHistory
        {
            Id = Guid.NewGuid(), PaymentId = payment.Id, OldStatus = payment.Status, NewStatus = "SubmittedToOwner",
            ChangedByUserId = userId, Note = request.Note, CreatedAt = now
        });
        payment.Status = "SubmittedToOwner";
        payment.SubmittedToOwnerAt = now;
        payment.UpdatedAt = now;
        await _repository.SaveChangesAsync();
        return ServiceResult.Ok(new { payment.Id, payment.OrderId, payment.Amount, payment.Status, payment.SubmittedToOwnerAt });
    }
}
