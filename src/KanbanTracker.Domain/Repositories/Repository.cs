using KanbanTracker.Domain.Base;

namespace KanbanTracker.Domain.Repositories;

public class Repository<T> where T : BaseEntity
{
    private readonly List<T> _items = new();

    public void Add(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        _items.Add(item);
    }

    public void Remove(T item)
    {
        _items.Remove(item);
    }

    public T? GetById(Guid id)
        => _items.FirstOrDefault(x => x.Id == id);

    public IReadOnlyList<T> GetAll()
        => _items.AsReadOnly();

    public IReadOnlyList<T> Find(Func<T, bool> predicate)
        => _items.Where(predicate).ToList();

    public int Count => _items.Count;
}