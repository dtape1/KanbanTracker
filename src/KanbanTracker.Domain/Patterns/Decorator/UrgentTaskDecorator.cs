using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.Decorator;

public class UrgentTaskDecorator : TaskDecorator
{
    public DateTime Deadline { get; }

    public UrgentTaskDecorator(TaskItem task, DateTime deadline)
        : base(task)
    {
        Deadline = deadline;
        Priority = Priority.High;
    }

    public override string GetSummary() =>
        $"[ТЕРМІНОВО до {Deadline:dd.MM.yyyy}] {_wrapped.GetSummary()}";

    public override string ToString() =>
        $"⚡ [URGENT] {_wrapped}";
}