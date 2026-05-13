namespace KanbanTracker.Domain.Base;

public abstract class BaseEntity
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public override string ToString() => $"[{GetType().Name}] Id={Id}";

    public override bool Equals(object? obj)
    {
        if (obj is BaseEntity other)
            return Id == other.Id;
        return false;
    }

    public override int GetHashCode() => Id.GetHashCode();
}