using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;
using KanbanTracker.Domain.Services;

// ── Збираємо дошку ──────────────────────────────────────
var board = new Board("Мій проєкт");

var todo       = new Column("Todo");
var inProgress = new Column("In Progress");
var done       = new Column("Done");

board.AddColumn(todo);
board.AddColumn(inProgress);
board.AddColumn(done);

var david = new User("Давид", "david@example.com");
var anna  = new User("Анна", "anna@example.com");
board.AddMember(david);
board.AddMember(anna);

var task1 = new TaskItem("Зробити UML-діаграму", "Намалювати діаграму класів", Priority.High);
var task2 = new TaskItem("Написати класи", Priority.Medium);
var bug1  = new BugReport("Краш при збереженні", "Зберегти → закрити", "Дані є", "Дані губляться");

task1.Assign(david);
task2.Assign(anna);

todo.AddTask(task2);
todo.AddTask(bug1);
inProgress.AddTask(task1);
task1.MoveToStatus(KanbanStatus.InProgress);

// ── Серіалізація ────────────────────────────────────────
Console.WriteLine("=== Серіалізація у JSON ===");
var boardService = new BoardService();
boardService.SaveToFile(board, "board.json");

Console.WriteLine("\n=== Десеріалізація з JSON ===");
var loaded = boardService.LoadFromFile("board.json");
if (loaded != null)
{
    Console.WriteLine($"Завантажено дошку: {loaded.Name}");
    foreach (var col in loaded.Columns)
    {
        Console.WriteLine($"  Колонка: {col.Name} ({col.Tasks.Count} завдань)");
        foreach (var t in col.Tasks)
            Console.WriteLine($"    - [{t.Priority}] {t.Title} | {t.Status} | Виконавець: {t.AssigneeName ?? "—"}");
    }
}