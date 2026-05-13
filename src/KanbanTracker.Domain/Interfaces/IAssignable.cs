using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Interfaces;

public interface IAssignable
{
    void Assign(User user);
    void Unassign();
}