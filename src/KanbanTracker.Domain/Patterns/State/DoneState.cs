using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.State;

public class DoneState : ITaskState
{
    public string StateName => "Done";

    public void MoveNext(TaskItem task)
    {
        Console.WriteLine($"  '{task.Title}': вже завершено, далі нікуди.");
    }

    public void MovePrev(TaskItem task)
    {
        Console.WriteLine($"  '{task.Title}': Done → Review");
        task.MoveToStatus(KanbanStatus.Review);
    }
}