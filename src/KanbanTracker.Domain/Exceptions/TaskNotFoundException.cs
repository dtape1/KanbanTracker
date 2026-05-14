namespace KanbanTracker.Domain.Exceptions;

public class TaskNotFoundException : DomainException
{
    public Guid TaskId { get; }

    public TaskNotFoundException(Guid id)
        : base($"Завдання з Id={id} не знайдено.")
    {
        TaskId = id;
    }
}