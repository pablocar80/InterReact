namespace InterReact;

public sealed class TickSnapshotEnd : IHasRequestId
{
    public int RequestId { get; }
    internal TickSnapshotEnd(ResponseReader r)
    {
        r.IgnoreMessageVersion();
        RequestId = r.ReadInt();
    }
};
