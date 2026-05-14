namespace KanbanTracker.Domain.Patterns.Composite;

public class TaskComposite : ITaskComponent
{
    private readonly List<ITaskComponent> _children = new();

    public string Title { get; }

    public TaskComposite(string title)
    {
        Title = title;
    }

    public void Add(ITaskComponent component)
        => _children.Add(component);

    public void Display(int depth = 0)
    {
        Console.WriteLine($"{new string(' ', depth * 2)}▶ {Title}");
        foreach (var child in _children)
            child.Display(depth + 1);
    }

    public int GetTotalCount()
        => _children.Sum(c => c.GetTotalCount());
}