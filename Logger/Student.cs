using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger;
public record class Student(Guid Id, string FirstName, string LastName, string Email) : IEntity
{
    public string FirstName { get; } = FirstName??throw new ArgumentNullException(nameof(FirstName));
    public string LastName { get; } = LastName??throw new ArgumentNullException(nameof(LastName));
    public string Email { get; } = Email??throw new ArgumentNullException(nameof(Email));

    public string Name { get => FirstName + " " + LastName; }
}
{
}
