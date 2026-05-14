using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.Factory;

public class BugReportFactory : ITaskFactory
{
    private readonly string _expected;
    private readonly string _actual;

    public BugReportFactory(string expected, string actual)
    {
        _expected = expected;
        _actual = actual;
    }

    public TaskItem Create(string title, string description)
        => new BugReport(title, description, _expected, _actual);
}