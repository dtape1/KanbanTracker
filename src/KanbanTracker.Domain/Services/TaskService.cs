using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Services;

public class TaskService
{
    // Делегат для події зміни статусу
    public delegate void TaskStatusChangedHandler(TaskItem task, KanbanStatus oldStatus, KanbanStatus newStatus);
    public event TaskStatusChangedHandler? OnStatusChanged;

    // Делегат для фільтрації
    public delegate bool TaskFilter(TaskItem task);

    private readonly List<TaskItem> _tasks = new();

    public void AddTask(TaskItem task)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));
        _tasks.Add(task);
    }

    public void ChangeStatus(TaskItem task, KanbanStatus newStatus)
    {
        var oldStatus = task.Status;
        task.MoveToStatus(newStatus);
        OnStatusChanged?.Invoke(task, oldStatus, newStatus);
    }

    // LINQ — фільтрація за статусом
    public IEnumerable<TaskItem> GetByStatus(KanbanStatus status)
        => _tasks.Where(t => t.Status == status);

    // LINQ — фільтрація за пріоритетом
    public IEnumerable<TaskItem> GetByPriority(Priority priority)
        => _tasks.Where(t => t.Priority == priority);

    // LINQ — завдання конкретного виконавця
    public IEnumerable<TaskItem> GetByAssignee(User user)
        => _tasks.Where(t => t.Assignee?.Id == user.Id);

    // LINQ — сортування за пріоритетом (High першими)
    public IEnumerable<TaskItem> GetSortedByPriority()
        => _tasks.OrderByDescending(t => t.Priority);

    // LINQ — статистика по статусах
    public Dictionary<KanbanStatus, int> GetStatusStats()
        => _tasks.GroupBy(t => t.Status)
                 .ToDictionary(g => g.Key, g => g.Count());

    // Метод з делегатом-фільтром
    public IEnumerable<TaskItem> Filter(TaskFilter filter)
        => _tasks.Where(t => filter(t));

    // LINQ — чи є хоч одне завдання в статусі
    public bool HasTasksWithStatus(KanbanStatus status)
        => _tasks.Any(t => t.Status == status);

    // LINQ — кількість завдань без виконавця
    public int CountUnassigned()
        => _tasks.Count(t => t.Assignee == null);
}