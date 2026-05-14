using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.Factory;

public class RegularTaskFactory : ITaskFactory
{
    private readonly Priority _priority;

    public RegularTaskFactory(Priority priority = Priority.Medium)
    {
        _priority = priority;
    }

    public TaskItem Create(string title, string description)
        => new TaskItem(title, description, _priority);
}