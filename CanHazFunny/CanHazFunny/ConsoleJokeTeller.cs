using System;

namespace CanHazFunny;

public class ConsoleJokeTeller : IJokeTeller
{
    public void TellJoke(string joke)
    {
        Console.WriteLine(joke);
    }
}
