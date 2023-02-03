namespace Logger;

public interface ILogger
{
    string LogSource { get; } // Many of you refer to this as the ClassName.
    void Log(LogLevel logLevel, string message);

    static abstract ILogger CreateLogger(in ILoggerConfiguration configuration);
}
