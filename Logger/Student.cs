namespace Logger;
public record class Student(Guid Id, string FirstName, string LastName, string Email, string? MiddleName = null, string? Major = null) 
    : Person(Id, FirstName, LastName, Email, MiddleName), IEntity
{
    public Student(string FirstName, string LastName, string Email, string? MiddleName = null, string? Major = null) 
        : this(Guid.NewGuid(), FirstName, LastName, Email, MiddleName, Major) { }
    public string Name { get => FirstName + " " + LastName; }
}
