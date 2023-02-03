using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Logger.Tests;

[TestClass]
public class LoggerMixinsTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Error_WithNullLogger_ThrowsException()
    {
        LoggerMixins.Error(null!, "");
    }

    [TestMethod]
    public void Error_WithData_LogsMessage()
    {
        // Arrange
        var logger = new TestLogger(nameof(LoggerMixinsTests));

        // Act
        logger.Error("Message 42");

        // Assert
        Assert.AreEqual(1, logger.LoggedMessages.Count);
        Assert.AreEqual(LogLevel.Error, logger.LoggedMessages[0].LogLevel);
        Assert.AreEqual("Message 42", logger.LoggedMessages[0].Message);
    }

}
