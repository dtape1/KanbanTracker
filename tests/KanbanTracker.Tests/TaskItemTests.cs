using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Tests;

public class TaskItemTests
{
    [Fact]
    public void TaskItem_CreatedWithTodoStatus()
    {
        var task = new TaskItem("Тест", Priority.Medium);
        Assert.Equal(KanbanStatus.Todo, task.Status);
    }

    [Fact]
    public void TaskItem_TitleCannotBeEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
            new TaskItem("", Priority.Low));
    }

    [Fact]
    public void TaskItem_AssignUser_SetsAssignee()
    {
        var task = new TaskItem("Тест", Priority.Medium);
        var user = new User("Давид", "d@test.com");
        task.Assign(user);
        Assert.Equal(user.Id, task.Assignee!.Id);
    }

    [Fact]
    public void TaskItem_Unassign_ClearsAssignee()
    {
        var task = new TaskItem("Тест", Priority.Medium);
        var user = new User("Давид", "d@test.com");
        task.Assign(user);
        task.Unassign();
        Assert.Null(task.Assignee);
    }

    [Fact]
    public void TaskItem_MoveToStatus_ChangesStatus()
    {
        var task = new TaskItem("Тест", Priority.Medium);
        task.MoveToStatus(KanbanStatus.InProgress);
        Assert.Equal(KanbanStatus.InProgress, task.Status);
    }

    [Fact]
    public void TaskItem_AddSubtask_WorksCorrectly()
    {
        var parent = new TaskItem("Батьківське", Priority.High);
        var child  = new TaskItem("Дочірнє", Priority.Low);
        parent.AddSubtask(child);
        Assert.Single(parent.Subtasks);
        Assert.Equal(child.Id, parent.Subtasks[0].Id);
    }

    [Fact]
    public void TaskItem_AddSelfAsSubtask_ThrowsException()
    {
        var task = new TaskItem("Тест", Priority.Medium);
        Assert.Throws<InvalidOperationException>(() =>
            task.AddSubtask(task));
    }

    [Fact]
    public void TaskItem_MatchesFilter_ReturnsCorrectResult()
    {
        var task = new TaskItem("Тест", Priority.Medium);
        task.MoveToStatus(KanbanStatus.Review);
        Assert.True(task.MatchesFilter(KanbanStatus.Review));
        Assert.False(task.MatchesFilter(KanbanStatus.Done));
    }
}