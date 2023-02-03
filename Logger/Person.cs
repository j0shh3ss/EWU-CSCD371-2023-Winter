namespace Logger;

public record Person(Guid Id, FullName FullName, string Email)
{
    public Person(Guid Id, string FirstName, string LastName, string Email, string? MiddleName = null)
    : this(Id, new FullName(FirstName, LastName, MiddleName), Email) { }
    public Person(string FirstName, string LastName, string Email, string? MiddleName = null)
        : this(Guid.NewGuid(), new FullName(FirstName, LastName, MiddleName), Email) { }
    public string Email { get; } = Email??throw new ArgumentNullException(nameof(Email));
    public string FirstName { get; } = FullName.FirstName??throw new ArgumentNullException(nameof(FirstName));
    public string LastName { get; } = FullName.LastName??throw new ArgumentNullException(nameof(LastName));
}
