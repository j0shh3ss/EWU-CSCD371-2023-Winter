namespace Logger.Tests;

public class TestLogger : ILogger
{
    public TestLogger(string logSource)
    {
        LogSource=logSource;
    }
    public List<(LogLevel LogLevel, string Message)> LoggedMessages { get; } = new List<(LogLevel, string)>();

    public string LogSource { get; }

    public static ILogger CreateLogger(in TestLoggerConfiguration configuration) => 
        new TestLogger(configuration.LogSource);

    static ILogger ILogger.CreateLogger(in ILoggerConfiguration configuration) => 
        configuration is TestLoggerConfiguration testLoggerConfiguration
            ? CreateLogger(testLoggerConfiguration)
            : throw new ArgumentException("Invalid configuration type", nameof(configuration));

    public void Log(LogLevel logLevel, string message)
    {
        LoggedMessages.Add((logLevel, message));
    }
}

public class TestLoggerConfiguration : ILoggerConfiguration
{
    public TestLoggerConfiguration(string className) => LogSource=className;

    public string LogSource { get; set; }
    
}
