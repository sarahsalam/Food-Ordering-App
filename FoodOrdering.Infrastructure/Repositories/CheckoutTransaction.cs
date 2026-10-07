using Microsoft.EntityFrameworkCore.Storage;

namespace FoodOrdering.Infrastructure.Repositories;

public class CheckoutTransaction : ICheckoutTransaction
{
    private readonly IDbContextTransaction _transaction;

    public CheckoutTransaction(IDbContextTransaction transaction)
    {
        _transaction = transaction;
    }

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return _transaction.CommitAsync(cancellationToken);
    }

    public Task RollbackAsync(CancellationToken cancellationToken)
    {
        return _transaction.RollbackAsync(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _transaction.DisposeAsync();
    }
}
