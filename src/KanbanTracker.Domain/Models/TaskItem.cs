using KanbanTracker.Domain.Base;
using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Interfaces;

namespace KanbanTracker.Domain.Models;

public class TaskItem : BaseEntity, IAssignable, IFilterable
{
    private string _title;
    private string _description;
    private readonly List<TaskItem> _subtasks = new();

    public string Title
    {
        get => _title;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва завдання не може бути порожньою.");
            _title = value;
        }
    }

    public string Description
    {
        get => _description;
        set => _description = value ?? string.Empty;
    }

    public Priority Priority { get; set; }
    public KanbanStatus Status { get; private set; }
    public User? Assignee { get; private set; }
    public IReadOnlyList<TaskItem> Subtasks => _subtasks.AsReadOnly();

    public TaskItem(string title, Priority priority)
    {
        Title = title;
        Priority = priority;
        Status = KanbanStatus.Todo;
        _description = string.Empty;
    }

    public TaskItem(string title, string description, Priority priority)
        : this(title, priority)
    {
        Description = description;
    }

    public void Assign(User user)
    {
        Assignee = user ?? throw new ArgumentNullException(nameof(user));
    }

    public void Unassign()
    {
        Assignee = null;
    }

    public void MoveToStatus(KanbanStatus newStatus)
    {
        Status = newStatus;
    }

    public void AddSubtask(TaskItem task)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));
        if (task.Id == Id) throw new InvalidOperationException("Завдання не може бути підзадачею самого себе.");
        _subtasks.Add(task);
    }

    public bool MatchesFilter(KanbanStatus status) => Status == status;

    public override string GetSummary() =>
        $"Завдання: {Title} | Пріоритет: {Priority} | Статус: {Status} | Виконавець: {Assignee?.Name ?? "—"}";

    public override string ToString() => $"[{Priority}] {Title} — {Status}";
}