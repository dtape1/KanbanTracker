namespace KanbanTracker.Domain.Patterns.Composite;

public interface ITaskComponent
{
    string Title { get; }
    void Display(int depth = 0);
    int GetTotalCount();
}