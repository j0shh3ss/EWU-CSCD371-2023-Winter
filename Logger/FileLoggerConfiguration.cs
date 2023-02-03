namespace Logger;

public class FileLoggerConfiguration : ILoggerConfiguration
{
    public FileLoggerConfiguration(string fileName, string className)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException($"'{nameof(fileName)}' cannot be null or whitespace.", nameof(fileName));
        }

        if (string.IsNullOrWhiteSpace(className))
        {
            throw new ArgumentException($"'{nameof(className)}' cannot be null or whitespace.", nameof(className));
        }

        FilePath = fileName;
        LogSource = className;
    }
    
    public string FilePath { get;  }
    public string LogSource { get; }
}
