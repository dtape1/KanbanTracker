using KanbanTracker.Domain.Enums;

namespace KanbanTracker.Domain.Models;

public class BugReport : TaskItem
{
    public string StepsToReproduce { get; set; }
    public string ExpectedBehavior { get; set; }
    public string ActualBehavior { get; set; }

    public BugReport(string title, string stepsToReproduce, string expected, string actual)
        : base(title, Priority.High)
    {
        StepsToReproduce = stepsToReproduce;
        ExpectedBehavior = expected;
        ActualBehavior = actual;
    }

    public override string GetSummary() =>
        $"Баг: {Title} | Очікувано: {ExpectedBehavior} | Фактично: {ActualBehavior}";

    public override string ToString() =>
        $"[BUG][{Priority}] {Title} — {Status}";
}