using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Extensions;

public static class TaskExtensions
{
    public static IEnumerable<TaskItem> FilterByStatus(
        this IEnumerable<TaskItem> tasks, KanbanStatus status)
        => tasks.Where(t => t.Status == status);

    public static IEnumerable<TaskItem> FilterByPriority(
        this IEnumerable<TaskItem> tasks, Priority priority)
        => tasks.Where(t => t.Priority == priority);

    public static IEnumerable<TaskItem> Unassigned(
        this IEnumerable<TaskItem> tasks)
        => tasks.Where(t => t.Assignee == null);

    public static IEnumerable<TaskItem> AssignedTo(
        this IEnumerable<TaskItem> tasks, User user)
        => tasks.Where(t => t.Assignee?.Id == user.Id);

    public static string ToSummaryList(this IEnumerable<TaskItem> tasks)
        => string.Join("\n", tasks.Select(t => $"  • {t.GetSummary()}"));
}