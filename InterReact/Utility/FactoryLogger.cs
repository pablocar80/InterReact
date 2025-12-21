namespace InterReact;

public static partial class Extension
{
    public static ILoggerFactory ToLoggerFactory(this ILogger logger) => new FactoryLogger(logger);
}

internal sealed class FactoryLogger : ILoggerFactory
{
    private readonly ILogger Logger;
    internal FactoryLogger(ILogger logger) => Logger = logger;

    public void AddProvider(ILoggerProvider provider) => throw new NotImplementedException();

    /// <summary>
    /// Simply returns the logger passed in the constructor.
    /// Does not actually create a logger of categoryName.
    /// </summary>
    /// <param name="ignoredCategoryName"></param>
    /// <returns></returns>
    public ILogger CreateLogger(string ignoredCategoryName = "") => Logger;

    public void Dispose() { }
}
