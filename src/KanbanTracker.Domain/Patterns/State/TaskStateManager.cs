using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.State;

public class TaskStateManager
{
    private readonly Dictionary<KanbanStatus, ITaskState> _states = new()
    {
        { KanbanStatus.Todo,       new TodoState() },
        { KanbanStatus.InProgress, new InProgressState() },
        { KanbanStatus.Review,     new ReviewState() },
        { KanbanStatus.Done,       new DoneState() }
    };

    private ITaskState GetState(TaskItem task)
        => _states[task.Status];

    public void MoveNext(TaskItem task)
        => GetState(task).MoveNext(task);

    public void MovePrev(TaskItem task)
        => GetState(task).MovePrev(task);

    public string GetStateName(TaskItem task)
        => GetState(task).StateName;
}