using KanbanTracker.Domain.Base;

namespace KanbanTracker.Domain.Models;

public class Epic : BaseEntity
{
    private string _title;
    private readonly List<TaskItem> _tasks = new();

    public string Title
    {
        get => _title;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва епіку не може бути порожньою.");
            _title = value;
        }
    }

    public IReadOnlyList<TaskItem> Tasks => _tasks.AsReadOnly();

    public Epic(string title)
    {
        Title = title;
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

    public override string ToString() => $"Епік: {Title} ({_tasks.Count} завдань)";
}