namespace InterReact;

public sealed class GenericTick : TickBase
{
    public double Value { get; }

    internal GenericTick(ResponseReader r)
    {
        r.IgnoreMessageVersion();
        RequestId = r.ReadInt();
        TickType = r.ReadEnum<TickType>();
        Value = r.ReadDouble();
    }
};
