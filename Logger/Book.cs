namespace Logger;
public record class Book(Guid Id, string Title, string Author) : IEntity
{
    public Book(string Title, string Author) : this(Guid.NewGuid(), Title, Author) { }

    public string Title { get; } = Title??throw new ArgumentNullException(nameof(Title));
    public string Author { get; } = Author??throw new ArgumentNullException(nameof(Author));
    public string Name { get => Title; }
}
