using KanbanTracker.Domain.Base;

namespace KanbanTracker.Domain.Models;

public class Board : BaseEntity
{
    private string _name;
    private readonly List<Column> _columns = new();
    private readonly List<User> _members = new();

    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва дошки не може бути порожньою.");
            _name = value;
        }
    }

    public IReadOnlyList<Column> Columns => _columns.AsReadOnly();
    public IReadOnlyList<User> Members => _members.AsReadOnly();

    public Board(string name)
    {
        Name = name;
    }

    public void AddColumn(Column column)
    {
        if (column == null) throw new ArgumentNullException(nameof(column));
        _columns.Add(column);
    }

    public void RemoveColumn(Column column)
    {
        _columns.Remove(column);
    }

    public void AddMember(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (!_members.Contains(user))
            _members.Add(user);
    }

    public void MoveTask(TaskItem task, Column from, Column to)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));
        from.RemoveTask(task);
        to.AddTask(task);
    }

    public override string ToString() => $"Дошка: {Name} ({_columns.Count} колонок)";
}