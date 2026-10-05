namespace FoodOrdering.Api.Repositories.Interfaces;

public interface ICheckoutTransaction : IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
}
