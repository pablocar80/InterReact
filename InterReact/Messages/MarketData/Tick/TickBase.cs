namespace InterReact;

public abstract class TickBase : IHasRequestId
{
    public int RequestId { get; protected set; }
    public TickType TickType { get; protected set; } = TickType.Undefined;
}
