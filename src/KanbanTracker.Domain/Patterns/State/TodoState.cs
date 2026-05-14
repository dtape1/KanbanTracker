using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.State;

public class TodoState : ITaskState
{
    public string StateName => "Todo";

    public void MoveNext(TaskItem task)
    {
        task.MoveToStatus(KanbanStatus.InProgress);
        Console.WriteLine($"  '{task.Title}': Todo → InProgress");
    }

    public void MovePrev(TaskItem task)
    {
        Console.WriteLine($"  '{task.Title}': вже на початку, назад неможливо.");
    }
}