namespace Logger;
public abstract class EntityBase : IEntity
{
    public Guid Id { get; init; }
    public abstract string Name { get; }
}
