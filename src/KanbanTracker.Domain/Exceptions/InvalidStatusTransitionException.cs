    using KanbanTracker.Domain.Enums;

namespace KanbanTracker.Domain.Exceptions;

public class InvalidStatusTransitionException : DomainException
{
    public InvalidStatusTransitionException(KanbanStatus from, KanbanStatus to)
        : base($"Неможливо перейти зі статусу {from} у {to}.") { }
}