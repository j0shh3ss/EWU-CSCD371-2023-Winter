using System.Net.Http;
using System.Text.Json;

namespace CanHazFunny;

public class JokeService: IJokeService
{
    private HttpClient HttpClient { get; } = new();

    public string GetJoke()
    {
        string json = HttpClient.GetStringAsync("https://geek-jokes.sameerkumar.website/api?format=json").Result;
        var joke = JsonSerializer.Deserialize<JokeInformation>(json);
        if (joke != null && joke.Joke != null) return joke.Joke;
        throw new System.Exception("No joke received from API or joke was null");
    }
}
