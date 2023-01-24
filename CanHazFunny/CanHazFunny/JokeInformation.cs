using System.Text.Json.Serialization;

namespace CanHazFunny;

public class JokeInformation
{
    [JsonPropertyName("joke")] 
    public string? Joke { get; set; }
}
