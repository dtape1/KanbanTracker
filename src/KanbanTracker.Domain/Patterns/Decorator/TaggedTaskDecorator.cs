using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.Decorator;

public class TaggedTaskDecorator : TaskDecorator
{
    private readonly List<string> _tags;

    public IReadOnlyList<string> Tags => _tags.AsReadOnly();

    public TaggedTaskDecorator(TaskItem task, params string[] tags)
        : base(task)
    {
        _tags = tags.ToList();
    }

    public override string GetSummary() =>
        $"{_wrapped.GetSummary()} | Теги: [{string.Join(", ", _tags)}]";

    public override string ToString() =>
        $"{_wrapped} 🏷 [{string.Join(", ", _tags)}]";
}