namespace Logger;
public abstract class EntityBase : IEntity
{
    public Guid Id { get; }
    public abstract string Name { get; }
}
