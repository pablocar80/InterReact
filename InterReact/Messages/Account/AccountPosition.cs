namespace InterReact;

public sealed class AccountPosition
{
    public bool IsEndMessage { get; }
    public string Account { get; }
    public Contract Contract { get; }
    public decimal Position { get; }
    public double AverageCost { get; }
    internal AccountPosition(ResponseReader r, bool isEndMessage)
    {
        if (isEndMessage)
        {
            r.IgnoreMessageVersion();
            IsEndMessage = true;
            Account = "";
            Contract = new();
            return;
        }
        r.RequireMessageVersion(3);
        Account = r.ReadString();
        Contract = new(r, includePrimaryExchange: false);
        Position = r.ReadDecimal();
        AverageCost = r.ReadDouble();
    }
}
