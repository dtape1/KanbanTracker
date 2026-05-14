using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.State;

public interface ITaskState
{
    void MoveNext(TaskItem task);
    void MovePrev(TaskItem task);
    string StateName { get; }
}