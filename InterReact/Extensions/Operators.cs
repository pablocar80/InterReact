namespace InterReact;

public static partial class Extension
{
    internal static IObservable<IHasRequestId> WithRequestId(this IObservable<object> source, int requestId) =>
        source
            .OfType<IHasRequestId>()
            .Where(m => m.RequestId == requestId);

    internal static IObservable<IHasOrderId> WithOrderId(this IObservable<object> source, int orderId) =>
        source
            .OfType<IHasOrderId>()
            .Where(m => m.OrderId == orderId);

    public static IObservable<T> OfTypeOnly<T>(this IObservable<object> source) =>
        Observable.Create<T>(observer =>
        {
            return source.Subscribe(
                m =>
                {
                    if (m is T t)
                        observer.OnNext(t);
                    else if (m is Alert a)
                        observer.OnError(a.ToAlertException());
                    else
                        observer.OnError(new InvalidOperationException($"Unexpected type: {m.GetType().Name}."));
                },
                observer.OnError,
                observer.OnCompleted);
        });

}
