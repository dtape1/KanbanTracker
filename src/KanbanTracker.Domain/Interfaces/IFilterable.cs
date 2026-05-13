using KanbanTracker.Domain.Enums;

namespace KanbanTracker.Domain.Interfaces;

public interface IFilterable
{
    bool MatchesFilter(KanbanStatus status);
}