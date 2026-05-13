using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;

var board = new Board("Мій проєкт");

var todo = new Column("Todo");
var inProgress = new Column("In Progress");
var done = new Column("Done");

board.AddColumn(todo);
board.AddColumn(inProgress);
board.AddColumn(done);

var david = new User("Давид", "david@example.com");
board.AddMember(david);

var task1 = new TaskItem("Зробити UML-діаграму", "Намалювати діаграму класів", Priority.High);
var task2 = new TaskItem("Написати класи", Priority.Medium);

task1.Assign(david);
todo.AddTask(task1);
todo.AddTask(task2);

board.MoveTask(task1, todo, inProgress);

Console.WriteLine(board);
foreach (var col in board.Columns)
{
    Console.WriteLine($"\n{col}");
    foreach (var t in col.Tasks)
        Console.WriteLine($"  - {t} | Виконавець: {t.Assignee?.Name ?? "—"}");
}