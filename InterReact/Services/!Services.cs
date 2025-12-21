namespace InterReact;

public partial class Service : IDisposable
{
    private readonly InterReactOptions Options;
    private readonly Request Request;
    private readonly Response Response;
    private bool Disposed;

    public Service(InterReactOptions options, Request request, Response response)
    {
        Options = options;
        Request = request;
        Response = response;
        AccountPositionsObservable = CreateAccountPositionsObservable();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (Disposed)
            return;

        if (disposing)
        {
            // dispose managed objects
            ManagedAccountsSemaphore.Dispose();
        }

        Disposed = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
