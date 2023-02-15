namespace Logger.Tests;
public static class ApplicationLogFactory
{
    public static LogFactory<TestLogger, TestLoggerFactory> LogFactory { get; } = new();

}


public class MyType
{
    private ILogger Logger = ApplicationLogFactory.LogFactory.CreateLogger(nameof(MyType));
}
