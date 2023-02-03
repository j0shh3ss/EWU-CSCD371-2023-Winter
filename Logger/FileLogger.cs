namespace Logger;

public class FileLogger : ILogger
{
    public FileLogger(string logSource, string filePath)
    {
        if (string.IsNullOrWhiteSpace(logSource))
        {
            throw new ArgumentException($"'{nameof(logSource)}' cannot be null or whitespace.", nameof(logSource));
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
        }

        LogSource = logSource;
        File = new FileInfo(filePath);
    }

    public FileLogger(FileLoggerConfiguration configuration)
    {
        (LogSource, File) = (configuration.LogSource, new FileInfo(configuration.FilePath));
    }

    public string LogSource { get; }

    FileInfo File { get; }

    public string FilePath { get => File.FullName; }

    static ILogger ILogger.CreateLogger(in ILoggerConfiguration logggerConfiguration) => 
        logggerConfiguration is FileLoggerConfiguration configuration
            ? CreateLogger(configuration)
            : throw new ArgumentException("Invalid configuration type", nameof(logggerConfiguration));
    
    public static FileLogger CreateLogger(FileLoggerConfiguration configuration)
    {
        return new FileLogger(configuration);
    }

    public void Log(LogLevel logLevel, string message)
    {
        using StreamWriter writer = File.AppendText();
        writer.WriteLine($"{ DateTime.Now },{LogSource},{logLevel},{message}");
    }
}
