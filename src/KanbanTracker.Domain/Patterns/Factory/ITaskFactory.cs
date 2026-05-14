using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.Factory;

public interface ITaskFactory
{
    TaskItem Create(string title, string description);
}