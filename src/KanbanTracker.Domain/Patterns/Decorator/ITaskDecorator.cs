using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.Decorator;

public abstract class TaskDecorator : TaskItem
{
    protected readonly TaskItem _wrapped;

    protected TaskDecorator(TaskItem task)
        : base(task.Title, task.Description, task.Priority)
    {
        _wrapped = task;
    }
}