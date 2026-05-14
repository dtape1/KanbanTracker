using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.State;

public class ReviewState : ITaskState
{
    public string StateName => "Review";

    public void MoveNext(TaskItem task)
    {
        task.MoveToStatus(KanbanStatus.Done);
        Console.WriteLine($"  '{task.Title}': Review → Done");
    }

    public void MovePrev(TaskItem task)
    {
        task.MoveToStatus(KanbanStatus.InProgress);
        Console.WriteLine($"  '{task.Title}': Review → InProgress");
    }
}