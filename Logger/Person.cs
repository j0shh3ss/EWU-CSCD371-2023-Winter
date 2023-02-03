namespace Logger;

public record Person(Guid Id, string FirstName, string LastName, string Email)
{
    public string Email { get; } = Email??throw new ArgumentNullException(nameof(Email));
    public string FirstName { get; } = FirstName??throw new ArgumentNullException(nameof(FirstName));
    public string LastName { get; } = LastName??throw new ArgumentNullException(nameof(LastName));
}
