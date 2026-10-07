namespace FoodOrdering.Application.Interfaces.Repositories;

public interface ICheckoutTransaction : IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
}
