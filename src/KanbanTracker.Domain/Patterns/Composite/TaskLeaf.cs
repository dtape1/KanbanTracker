using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Patterns.Composite;

public class TaskLeaf : ITaskComponent
{
    private readonly TaskItem _task;

    public string Title => _task.Title;

    public TaskLeaf(TaskItem task)
    {
        _task = task;
    }

    public void Display(int depth = 0)
    {
        Console.WriteLine($"{new string(' ', depth * 2)}• {_task}");
    }

    public int GetTotalCount() => 1;
}