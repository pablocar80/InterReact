using System.Reactive.Threading.Tasks;
namespace InterReact;

public partial class Service
{
    /// <summary>
    /// Sucessive ids used for requests and orders are generated using Request.GetNextId().
    /// In case there are multiple client applications connected to an account
    /// which are creating orders, the next id may be updated(increased) using UpdateNextIdAsync().
    /// https://interactivebrokers.github.io/tws-api/order_submission.html
    // IMPORTANT: The result is updated in the constructor of the NextOrderId response message.
    /// </summary>
    public async Task UpdateNextIdAsync(TimeSpan? timeout = null, CancellationToken ct = default) =>
        await Response
            .OfType<NextIdMessage>()
            .ToObservable(Request.RequestNextOrderId)
            .WithTimeout(timeout, ct)
            .SingleAsync();
}
