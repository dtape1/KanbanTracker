using KanbanTracker.Domain.Base;
using KanbanTracker.Domain.Enums;

namespace KanbanTracker.Domain.Models;

public class Column : BaseEntity
{
    private string _name;
    private readonly List<TaskItem> _tasks = new();

    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва колонки не може бути порожньою.");
            _name = value;
        }
    }

    public IReadOnlyList<TaskItem> Tasks => _tasks.AsReadOnly();

    public Column(string name)
    {
        Name = name;
    }

    public void AddTask(TaskItem task)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));
        _tasks.Add(task);
    }

    public void RemoveTask(TaskItem task)
    {
        _tasks.Remove(task);
    }

    public List<TaskItem> GetByStatus(KanbanStatus status)
        => _tasks.Where(t => t.MatchesFilter(status)).ToList();

    public override string ToString() => $"Колонка: {Name} ({_tasks.Count} завдань)";
}