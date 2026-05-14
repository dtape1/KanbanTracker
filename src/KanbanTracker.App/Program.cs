using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Exceptions;
using KanbanTracker.Domain.Extensions;
using KanbanTracker.Domain.Models;
using KanbanTracker.Domain.Patterns.Decorator;
using KanbanTracker.Domain.Services;

//Дошка
var board = new Board("Мій проєкт");
var todo       = new Column("Todo");
var inProgress = new Column("In Progress");
var done       = new Column("Done");
board.AddColumn(todo);
board.AddColumn(inProgress);
board.AddColumn(done);

var davyd = new User("Давид", "davyd@example.com");
var anna  = new User("Анна",  "anna@example.com");
board.AddMember(davyd);
board.AddMember(anna);

var task1 = new TaskItem("Зробити UML-діаграму", "Намалювати діаграму класів", Priority.High);
var task2 = new TaskItem("Написати класи", Priority.Medium);
var task3 = new TaskItem("Написати тести", Priority.High);
var task4 = new TaskItem("Налаштувати CI", Priority.Low);

task1.Assign(davyd);
task2.Assign(anna);
task3.Assign(davyd);

todo.AddTask(task1);
todo.AddTask(task2);
todo.AddTask(task3);
todo.AddTask(task4);

// Custom Exceptions
Console.WriteLine("=== Custom Exceptions ===");
try
{
    task1.Assign(davyd);
}
catch (UserAlreadyAssignedException ex)
{
    Console.WriteLine($"Помилка: {ex.Message}");
}

try
{
    var bad = new TaskItem("", Priority.Low);
}
catch (DomainException ex)
{
    Console.WriteLine($"Помилка: {ex.Message}");
}

// Extension Methods
Console.WriteLine("\n=== Extension Methods ===");
var allTasks = todo.Tasks.ToList();
Console.WriteLine("Завдання з High пріоритетом:");
Console.WriteLine(allTasks.FilterByPriority(Priority.High).ToSummaryList());

Console.WriteLine("\nБез виконавця:");
Console.WriteLine(allTasks.Unassigned().ToSummaryList());

Console.WriteLine("\nЗавдання Давида:");
Console.WriteLine(allTasks.AssignedTo(davyd).ToSummaryList());

//Перевантаження операторів
Console.WriteLine("\n=== Перевантаження операторів ===");
var davyd2 = davyd;
Console.WriteLine($"davyd == davyd2: {davyd == davyd2}");
Console.WriteLine($"davyd == anna:   {davyd == anna}");
Console.WriteLine($"davyd != anna:   {davyd != anna}");

// Decorator
Console.WriteLine("\n=== Патерн Decorator ===");
var urgentTask = new UrgentTaskDecorator(task2, DateTime.Now.AddDays(2));
var taggedTask = new TaggedTaskDecorator(task3, "backend", "testing", "ci");

Console.WriteLine(urgentTask.GetSummary());
Console.WriteLine(taggedTask.GetSummary());

// Серіалізація 
Console.WriteLine("\n=== Серіалізація у JSON ===");
board.MoveTask(task1, todo, inProgress);
task1.MoveToStatus(KanbanStatus.InProgress);

var boardService = new BoardService();
boardService.SaveToFile(board, "board.json");

var loaded = boardService.LoadFromFile("board.json");
if (loaded != null)
{
    Console.WriteLine($"Завантажено: {loaded.Name}");
    foreach (var col in loaded.Columns)
    {
        Console.WriteLine($"  {col.Name} ({col.Tasks.Count} завдань)");
        foreach (var t in col.Tasks)
            Console.WriteLine($"    - [{t.Priority}] {t.Title} | {t.Status}");
    }
}