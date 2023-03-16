using IntelliTect.TestTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.Tests;

[TestClass]
public class PingProcessTests
{
    PingProcess Sut { get; set; } = new();

    [TestInitialize]
    public void TestInitialize()
    {
        Sut = new();
    }

    [TestMethod]
    public void Start_PingProcess_Success()
    {
        Process process = Process.Start("ping", "localhost");
        process.WaitForExit();
        Assert.AreEqual<int>(0, process.ExitCode);
    }

    [TestMethod]
    public void Run_GoogleDotCom_Success()
    {
        int exitCode = Sut.Run("google.com").ExitCode;
        Assert.AreEqual<int>(0, exitCode);
    }


    [TestMethod]
    public void Run_InvalidAddressOutput_Success()
    {
        (int exitCode, string? stdOutput) = Sut.Run("badaddress");
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());
        Assert.AreEqual<string?>(
            "Ping request could not find host badaddress. Please check the name and try again.".Trim(),
            stdOutput,
            $"Output is unexpected: {stdOutput}");
        Assert.AreEqual<int>(1, exitCode);
    }

    [TestMethod]
    public void Run_CaptureStdOutput_Success()
    {
        PingResult result = Sut.Run("localhost");
        AssertValidPingOutput(result);
    }
    [TestMethod]
    public void RunTaskAsync_Success()
    {
        var pingProcess = new PingProcess();

        var task = pingProcess.RunTaskAsync("localhost");


        Thread.Sleep(100);

        task.Wait();

        Assert.IsTrue(task.IsCompletedSuccessfully);
        var result = task.Result;
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.ExitCode);
        Assert.IsTrue(result.StdOutput!.Contains("Reply"));
    }



    [TestMethod]
    public void RunAsync_UsingTaskReturn_Success()
    {
        var pingProcess = new PingProcess();
        var task = pingProcess.RunAsync("localhost", CancellationToken.None);

        task.Wait();

        Assert.IsTrue(task.IsCompletedSuccessfully);
        var result = task.Result;
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.ExitCode);
        Assert.IsTrue(result.StdOutput!.Contains("Reply"));
    }

    [TestMethod]
    public async Task RunAsync_UsingTpl_Success()
    {
        var pingProcess = new PingProcess();
        var result = await Task.Run(() => pingProcess.RunAsync("localhost"), CancellationToken.None);

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.ExitCode);
        Assert.IsTrue(result.StdOutput!.Contains("Reply"));
    }


    [TestMethod]
    public async Task RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrapping()
    {
        var pingProcess = new PingProcess();
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.CancelAfter(100); // cancel after 100 ms
        try
        {
            await pingProcess.RunAsync("google.com", cancellationTokenSource.Token);
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is TaskCanceledException)
            {
                throw;
            }
            else
            {
                throw new AssertFailedException("Expected TaskCanceledException inner exception");
            }
        }
    }

    [TestMethod]
    public async Task RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrappingTaskCanceledException()
    {
        var pingProcess = new PingProcess();
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.CancelAfter(100); // cancel after 100 ms
        try
        {
            await pingProcess.RunAsync("google.com", cancellationTokenSource.Token);
        }
        catch (TaskCanceledException)
        {
            // success
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is TaskCanceledException)
            {
                throw new AssertFailedException("Expected TaskCanceledException, not AggregateException wrapping TaskCanceledException");
            }
            else
            {
                throw new AssertFailedException("Expected TaskCanceledException inner exception");
            }
        }
    }




    [TestMethod]
    async public Task RunAsync_MultipleHostAddresses_True()
    {
        string[] hostNames = new string[] { "localhost", "localhost", "localhost", "localhost" };
        int expectedLineCount = PingOutputLikeExpression.Split(Environment.NewLine).Length * hostNames.Length;

        // execute test
        PingResult result = await Sut.RunAsync(hostNames);

        // assert
        int? lineCount = result.StdOutput?.Split(Environment.NewLine).Length;
        Assert.IsTrue(lineCount >= expectedLineCount);

    }

    [TestMethod]
#pragma warning disable CS1998 // Remove this
    async public Task RunLongRunningAsync_UsingTpl_Success()
    {
        PingResult result = default;
        // Test Sut.RunLongRunningAsync("localhost");
        AssertValidPingOutput(result);
    }
#pragma warning restore CS1998 // Remove this

    [TestMethod]
    public void StringBuilderAppendLine_InParallel_IsNotThreadSafe()
    {
        IEnumerable<int> numbers = Enumerable.Range(0, short.MaxValue);
        System.Text.StringBuilder stringBuilder = new();
        numbers.AsParallel().ForAll(item => stringBuilder.AppendLine(""));
        int lineCount = stringBuilder.ToString().Split(Environment.NewLine).Length;
        Assert.AreNotEqual(lineCount, numbers.Count()+1);
    }

    readonly string PingOutputLikeExpression = @"
Pinging * with 32 bytes of data:
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*

Ping statistics for ::1:
    Packets: Sent = *, Received = *, Lost = 0 (0% loss),
Approximate round trip times in milli-seconds:
    Minimum = *, Maximum = *, Average = *".Trim();
    private void AssertValidPingOutput(int exitCode, string? stdOutput)
    {
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());
        Assert.IsTrue(stdOutput?.IsLike(PingOutputLikeExpression)??false,
            $"Output is unexpected: {stdOutput}");
        Assert.AreEqual<int>(0, exitCode);
    }
    private void AssertValidPingOutput(PingResult result) =>
        AssertValidPingOutput(result.ExitCode, result.StdOutput);
}
