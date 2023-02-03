using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logger.Tests;

[TestClass]
public class FileLoggerTests : FileLoggerTestsBase
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected string FilePath { get; set; }
    protected FileLogger Logger { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [TestInitialize]
    public virtual void TestInitialize()
    {
        TestCleanup();
        FilePath = Path.GetTempFileName();
        Logger = new FileLogger(nameof(FileLoggerTests), FilePath);
    }

    [TestCleanup]
    public virtual void TestCleanup()
    {
        if (File.Exists(FilePath)) File.Delete(FilePath);
    }

    [TestMethod]
    public void CreateLogger_TestConfiguration_Success()
    {
        FileLogger fileLogger = FileLogger.CreateLogger(new FileLoggerConfiguration(nameof(FileLoggerTests), FilePath)) as FileLogger;
    }

    
    [TestMethod]
    public void Create_GivenClassAndValidFileName_Success()
    {
        Assert.AreEqual(nameof(FileLoggerTests), Logger.LogSource);
        Assert.AreEqual(FilePath, Logger.FilePath);
    }

    [TestMethod]
    public async Task Log_Message_FileAppended()
    {
        Logger.Log(LogLevel.Error, "Message1");
        Logger.Log(LogLevel.Error, "Message2");

        string[] lines = await File.ReadAllLinesAsync(FilePath);
        Assert.IsTrue(lines is [..] and { Length: 2 });
        foreach (string[] line in lines.Select(line => line.Split(',', 4)))
        {
            if (line is [string dateTime, string source, string levelText, string message])
            {
                Assert.IsTrue(DateTime.TryParse(dateTime, out _));
                Assert.AreEqual(nameof(FileLoggerTests), source);
                Assert.IsTrue(Enum.TryParse(typeof(LogLevel), levelText, out object? level) ?
                    level is LogLevel.Error : false,"Level was not parsed successfully.");
            }
        }
    }
}
