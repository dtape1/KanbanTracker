using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Tests;

public class BoardTests
{
    [Fact]
    public void Board_AddColumn_IncreasesCount()
    {
        var board = new Board("Тест");
        board.AddColumn(new Column("Todo"));
        Assert.Single(board.Columns);
    }

    [Fact]
    public void Board_AddMember_NoDuplicates()
    {
        var board = new Board("Тест");
        var user = new User("Давид", "d@test.com");
        board.AddMember(user);
        board.AddMember(user);
        Assert.Single(board.Members);
    }

    [Fact]
    public void Board_MoveTask_MovesCorrectly()
    {
        var board = new Board("Тест");
        var todo = new Column("Todo");
        var inProgress = new Column("InProgress");
        board.AddColumn(todo);
        board.AddColumn(inProgress);

        var task = new TaskItem("Завдання", Priority.Medium);
        todo.AddTask(task);
        board.MoveTask(task, todo, inProgress);

        Assert.Empty(todo.Tasks);
        Assert.Single(inProgress.Tasks);
    }

    [Fact]
    public void Board_NameCannotBeEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Board(""));
    }
}