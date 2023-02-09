namespace Logger;

public class FileLogger : BaseLogger, ILogger<FileLogger>
{
    private FileInfo File { get; }

    public string FilePath { get => File.FullName; }

    public FileLogger(string logSource, string filePath) : base(logSource) => File = new FileInfo(filePath);

    public FileLogger(FileLoggerConfiguration configuration) : this(configuration.LogSource, configuration.FilePath) {}
    
    public static FileLogger CreateLogger(in ILoggerConfiguration configuration)
    {
        FileLogger fileLogger = configuration is FileLoggerConfiguration fileLoggerConfiguration
                    ? CreateLogger(fileLoggerConfiguration)
                    : throw new ArgumentException("Invalid configuration type", nameof(fileLoggerConfiguration));
        return fileLogger;
    }

    public static FileLogger CreateLogger(FileLoggerConfiguration configuration) => new(configuration);

    public override void Log(LogLevel logLevel, string message)
    {
        using StreamWriter writer = File.AppendText();
        writer.WriteLine($"{ DateTime.Now },{LogSource},{logLevel},{message}");
    }
}
