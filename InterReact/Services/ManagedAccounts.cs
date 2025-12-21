using System.Diagnostics;
namespace InterReact;

public partial class Service
{
    private readonly SemaphoreSlim ManagedAccountsSemaphore = new(1, 1);

    public async Task<IReadOnlyList<string>> GetManagedAccountsAsync(TimeSpan? timeout = null, CancellationToken ct = default)
    {
        await ManagedAccountsSemaphore.WaitAsync(ct).ConfigureAwait(false);
        try
        {
            if (Options.ManagedAccounts.Count == 0)
            {
                // Options.ManagedAccounts is updated whenever a ManagedAccounts message is received.
                await Response
                    .OfType<ManagedAccounts>()
                    .ToObservable(Request.RequestManagedAccounts)
                    .Select(x => x.Accounts)
                    .WithTimeout(timeout, ct)
                    .SingleAsync();
            }

            Debug.Assert(Options.ManagedAccounts.Count > 0);

            return Options.ManagedAccounts;
        }
        finally
        {
            ManagedAccountsSemaphore.Release();
        }
    }
}
