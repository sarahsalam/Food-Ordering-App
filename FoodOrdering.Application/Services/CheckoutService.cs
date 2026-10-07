using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;

namespace FoodOrdering.Application.Services;

public sealed class CheckoutService : ICheckoutService
{
    private readonly ICheckoutRepository _repository;

    public CheckoutService(ICheckoutRepository repository)
    {
        _repository = repository;
    }

    public async Task<(bool Success, string? Error, OrderResponse? Data)> CheckoutAsync(
        Guid userId, CheckoutRequest request, CancellationToken cancellationToken = default)
    {
        if (request.BranchId == Guid.Empty || string.IsNullOrWhiteSpace(request.DeliveryAddress))
        {
            return (false, "Branch and delivery address are required.", null);
        }
        
        await using var transaction = await _repository.BeginTransactionAsync(cancellationToken);
        try
        {
            var cart = await _repository.GetCartAsync(userId, cancellationToken);
            if (cart == null)
            {
                return (false, "Cart was not found.", null);
            }
            if (cart.Items.Count == 0)
            {
                return (false, "Cart is empty.", null);
            }

            var branch = await _repository.GetBranchAsync(request.BranchId, cancellationToken);
            if (branch == null)
            {
                return (false, "Branch was not found.", null);
            }
            if (branch.Restaurant.ApprovalStatus != RestaurantApprovalStatus.Approved)
            {
                return (false, "Restaurant has not been approved to accept orders.", null);
            }

            if (!branch.IsActive || !branch.Restaurant.IsActive)
            {
                return (false, "Restaurant or branch is unavailable.", null);
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                RestaurantId = branch.RestaurantId,
                BranchId = branch.Id,
                Status = "Pending",
                DeliveryAddress = request.DeliveryAddress.Trim(),
                CreatedAt = DateTime.UtcNow
            };
            foreach (var item in cart.Items)
            {
                if (item.Quantity <= 0)
                {
                    return (false, "Cart quantities must be positive.", null);
                }
                if (!item.Product.IsActive)
                {
                    return (false, $"Product '{item.Product.Name}' is inactive.", null);
                }

                if (!item.Product.Category.IsActive)
                {
                    return (false, $"The category for product '{item.Product.Name}' is inactive.", null);
                }

                if (item.Product.Category.RestaurantId != branch.RestaurantId)
                {
                    return (false, $"Product '{item.Product.Name}' belongs to a different restaurant than the selected branch.", null);
                }

                var variant = item.ProductVariant;
                if (item.ProductVariantId.HasValue)
                {
                    if (variant == null)
                    {
                        return (false, "A selected variant is unavailable or does not belong to its product.", null);
                    }

                    if (!variant.IsActive || variant.ProductId != item.ProductId)
                    {
                        return (false, "A selected variant is unavailable or does not belong to its product.", null);
                    }
                }

                decimal price = item.Product.BasePrice;
                string? variantName = null;
                if (variant != null)
                {
                    price = variant.Price;
                    variantName = variant.Name;
                }

                if (price < 0)
                {
                    return (false, "Product prices cannot be negative.", null);
                }
                decimal addonPrice = 0;
                foreach (var selection in item.Addons)
                {
                    var addon = selection.Addon;
                    bool linked = await _repository.IsAddonLinkedAsync(item.ProductId, selection.AddonId, cancellationToken);
                    if (!addon.IsActive || addon.Price < 0 || !linked || addon.RestaurantId != branch.RestaurantId)
                    {
                        return (false, "A selected addon is inactive or no longer linked to its product.", null);
                    }
                    addonPrice += addon.Price;
                }
                var total = (price + addonPrice) * item.Quantity;

                const decimal maximumOrderTotal = 9999999999999999.99m;
                if (total > maximumOrderTotal - order.Subtotal)
                {
                    return (false, "Order total exceeds the supported amount.", null);
                }
                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    ProductVariantId = item.ProductVariantId,
                    ProductName = item.Product.Name,
                    VariantName = variantName,
                    UnitPrice = price,
                    Quantity = item.Quantity,
                    TotalPrice = total
                };

                foreach (var selection in item.Addons)
                {
                    orderItem.Addons.Add(new OrderItemAddon
                    {
                        Id = Guid.NewGuid(), OrderItemId = orderItem.Id, AddonId = selection.AddonId,
                        AddonName = selection.Addon.Name, UnitPrice = selection.Addon.Price,
                        Quantity = item.Quantity
                    });
                }
                order.Items.Add(orderItem);
                order.Subtotal += total;
            }

            UserCoupon? assignedCoupon = null;
            if (!string.IsNullOrWhiteSpace(request.CouponCode))
            {
                var code = request.CouponCode.Trim().ToUpperInvariant();
                assignedCoupon = await _repository.GetAssignedCouponAsync(userId, code, cancellationToken);
                if (assignedCoupon == null)
                {
                    return (false, "This coupon has not been assigned to you.", null);
                }
                var coupon = assignedCoupon.Coupon;
                var now = DateTime.UtcNow;
                if (assignedCoupon.IsUsed)
                {
                    return (false, "You have already used this coupon.", null);
                }
                if (!coupon.IsActive || now < coupon.StartDate || now >= coupon.ExpiryDate)
                {
                    return (false, "Coupon is inactive, not started, or expired.", null);
                }
                if (order.Subtotal < coupon.MinimumOrderAmount)
                {
                    return (false, "The subtotal does not meet this coupon's minimum order amount.", null);
                }
                if (coupon.UsageLimit.HasValue)
                {
                    var usageCount = await _repository.GetCouponUsageCountAsync(coupon.Id, cancellationToken);
                    if (usageCount >= coupon.UsageLimit.Value)
                    {
                        return (false, "This coupon has reached its usage limit.", null);
                    }
                }

                decimal discount = coupon.DiscountValue;
                if (coupon.DiscountType == "Percentage")
                {
                    discount = order.Subtotal * coupon.DiscountValue / 100m;
                    if (coupon.MaximumDiscount.HasValue && discount > coupon.MaximumDiscount.Value)
                    {
                        discount = coupon.MaximumDiscount.Value;
                    }
                }
                order.Discount = Math.Min(order.Subtotal, decimal.Round(discount, 2, MidpointRounding.AwayFromZero));
            }

            var discountedSubtotal = order.Subtotal - order.Discount;
            order.Tax = decimal.Round(discountedSubtotal * 0.10m, 2, MidpointRounding.AwayFromZero);
            order.DeliveryFee = 200m;
            order.TotalAmount = discountedSubtotal + order.Tax + order.DeliveryFee;
            if (order.TotalAmount > 9999999999999999.99m)
            {
                return (false, "Order total exceeds the supported amount.", null);
            }

            _repository.AddOrder(order);
            _repository.AddOrderHistory(new OrderStatusHistory
            {
                Id = Guid.NewGuid(), Order = order, NewStatus = "Pending",
                ChangedByUserId = userId, Note = "Order placed.", CreatedAt = order.CreatedAt
            });
            var payment = new Payment
            {
                Id = Guid.NewGuid(), Order = order, Amount = order.TotalAmount,
                PaymentMethod = "CashOnDelivery", Status = "Pending", CreatedAt = order.CreatedAt
            };
            _repository.AddPayment(payment);
            _repository.AddPaymentHistory(new PaymentHistory
            {
                Id = Guid.NewGuid(), Payment = payment, NewStatus = "Pending",
                ChangedByUserId = userId, Note = "Cash on delivery payment created.", CreatedAt = order.CreatedAt
            });
            if (assignedCoupon != null)
            {
                assignedCoupon.IsUsed = true;
                _repository.AddCouponUsage(new CouponUsage
                {
                    Id = Guid.NewGuid(), CouponId = assignedCoupon.CouponId, UserId = userId,
                    Order = order, DiscountAmount = order.Discount, UsedAt = order.CreatedAt
                });
            }
            _repository.RemoveCartItems(cart.Items);
            cart.UpdatedAt = DateTime.UtcNow;
            await _repository.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            var response = new OrderResponse(
                order.Id,
                order.UserId,
                order.RestaurantId,
                order.BranchId,
                order.Status,
                order.Subtotal,
                order.Tax,
                order.DeliveryFee,
                order.Discount,
                order.TotalAmount,
                order.DeliveryAddress,
                order.CreatedAt,
                order.UpdatedAt);

            return (true, null, response);
        }
        catch (Exception exception)
        {
            bool isConcurrencyConflict = _repository.IsConcurrencyConflict(exception);

            if (!isConcurrencyConflict)
            {
                throw;
            }

            await transaction.RollbackAsync(cancellationToken);
            _repository.ClearTracking();
            return (false, "CONFLICT:Cart or coupon changed during checkout. Please reload and try again.", null);
        }
    }
}
