namespace Logger;

public record class Employee(Guid Id, string FirstName, string LastName, string Email, string? MiddleName, string EmployeeId )
    : Person (Id, FirstName,LastName, Email, MiddleName), IEntity
{
    public string EmployeeId { get; } = EmployeeId??throw new ArgumentNullException(EmployeeId);
    public string Name { get => FirstName + " " + LastName; }

}