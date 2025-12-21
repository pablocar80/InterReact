using System.Reactive;
using System.Reactive.Disposables;
namespace InterReact;

public static partial class Extension
{
    public static IObservable<T> ToObservable<T>(
        this IObservable<T> source,
        Action startRequest)
    {
        return Observable.Create<T>(observer =>
        {
            IDisposable subscription = source
                .SubscribeSafe(Observer.Create<T>(
                    onNext: m =>
                    {
                        observer.OnNext(m);
                        observer.OnCompleted();
                    },
                    onError: observer.OnError,
                    onCompleted: observer.OnCompleted));

            startRequest();

            return subscription;
        });
    }

    public static IObservable<T> ToObservable<T>(
        this IObservable<object> source,
        Action startRequest, Action? stopRequest = null)
    {
        return Observable.Create<T>(observer =>
        {
            bool? cancelable = null;

            IDisposable subscription = source
                .OfType<T>()
                .SubscribeSafe(Observer.Create<T>(
                    onNext: observer.OnNext,
                    onError: e =>
                    {
                        cancelable = false;
                        observer.OnError(e);
                    },
                    onCompleted: () =>
                    {
                        cancelable = false;
                        observer.OnCompleted();
                    }));

            if (cancelable is null)
                startRequest();
            cancelable ??= true;

            return Disposable.Create(() =>
            {
                if (cancelable is true && stopRequest is not null)
                    stopRequest();
                subscription.Dispose();
            });
        });
    }

    // For continuous results with RequestId: AccountUpdatesMulti, MarketData
    // For multiple   results with RequestId: ContractDetails, MarketDataSnapshot
    public static IObservable<IHasRequestId> ToObservableWithId(
        this IObservable<object> source, 
        Func<int> getRequestId,
        Action<int> startRequest, Action<int>? stopRequest = null)
    {
        return Observable.Create<IHasRequestId>(observer =>
        {
            int id = getRequestId();
            bool? cancelable = null;

            IDisposable subscription = source
                .WithRequestId(id)
                .SubscribeSafe(Observer.Create<IHasRequestId>(
                    onNext: observer.OnNext,
                    onError: e =>
                    {
                        cancelable = false;
                        observer.OnError(e);
                    },
                    onCompleted: () =>
                    {
                        cancelable = false;
                        observer.OnCompleted();
                    }));

            if (cancelable is null)
                startRequest(id);
            cancelable ??= true;

            return Disposable.Create(() =>
            {
                if (cancelable is true && stopRequest is not null)
                    stopRequest(id);
                subscription.Dispose();
            });
        });
    }

}
