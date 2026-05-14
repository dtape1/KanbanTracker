using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.State;

public class InProgressState : ITaskState
{
    public string StateName => "InProgress";

    public void MoveNext(TaskItem task)
    {
        task.MoveToStatus(KanbanStatus.Review);
        Console.WriteLine($"  '{task.Title}': InProgress → Review");
    }

    public void MovePrev(TaskItem task)
    {
        task.MoveToStatus(KanbanStatus.Todo);
        Console.WriteLine($"  '{task.Title}': InProgress → Todo");
    }
}