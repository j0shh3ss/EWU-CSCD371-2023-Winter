namespace CanHazFunny.Tests;

[TestClass]
public class JesterTests
{
    [TestMethod]
    public void TestJokeService()
    {
        JokeService jokeService = new();
        Assert.IsNotNull(jokeService.GetJoke());
    }

    [TestMethod]
    public void TestJester()
    {
        JokeService jokeService = new();
        RecordingJokeTeller jokeTeller = new();
        new Jester(jokeService, jokeTeller).TellJoke();
        Assert.IsNotNull(jokeTeller.LastJoke);
    }

    [TestMethod]
    public void TestConsoleWriter()
    {
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        ConsoleJokeTeller consoleJokeTeller = new();
        var joke = "Funny Joke!";
        consoleJokeTeller.TellJoke(joke);
        Assert.AreEqual(joke + Environment.NewLine, stringWriter.ToString());
    }
}
