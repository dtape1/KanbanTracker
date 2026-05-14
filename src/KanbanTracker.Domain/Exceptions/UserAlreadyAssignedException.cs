namespace KanbanTracker.Domain.Exceptions;

public class UserAlreadyAssignedException : DomainException
{
    public UserAlreadyAssignedException(string userName)
        : base($"Користувач '{userName}' вже призначений на це завдання.") { }
}