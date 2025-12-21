namespace InterReact;

public sealed class ManagedAccounts
{
    public IReadOnlyList<string> Accounts { get; }

    internal ManagedAccounts(ResponseReader r)
    {
        r.IgnoreMessageVersion();
        Accounts = r.ReadString().Split(',', StringSplitOptions.RemoveEmptyEntries);
        r.Options.ManagedAccounts = Accounts;
    }
}
