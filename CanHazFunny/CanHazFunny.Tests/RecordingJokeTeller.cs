namespace CanHazFunny.Tests;

public class RecordingJokeTeller : IJokeTeller
{
    public string? LastJoke { get; internal set; }

    public void TellJoke(string joke)
    {
        LastJoke = joke;
    }
}
