using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;
using KanbanTracker.Domain.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

// ── Початкові дані ───────────────────────────────────────
var board = new Board("Мій проєкт");

var todo       = new Column("Todo");
var inProgress = new Column("In Progress");
var review     = new Column("Review");
var done       = new Column("Done");

board.AddColumn(todo);
board.AddColumn(inProgress);
board.AddColumn(review);
board.AddColumn(done);

var davyd = new User("Давид", "davyd@example.com");
var anna  = new User("Анна",  "anna@example.com");
board.AddMember(davyd);
board.AddMember(anna);

var task1 = new TaskItem("Зробити UML-діаграму", "Намалювати діаграму класів", Priority.High);
var task2 = new TaskItem("Написати класи", Priority.Medium);
var task3 = new TaskItem("Написати тести", Priority.High);
var bug1  = new BugReport("Краш при збереженні", "Зберегти → закрити", "Дані є", "Дані губляться");

task1.Assign(davyd);
task2.Assign(anna);
task3.Assign(davyd);

todo.AddTask(task1);
todo.AddTask(task2);
todo.AddTask(task3);
todo.AddTask(bug1);

// ── Меню ────────────────────────────────────────────────
while (true)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════╗");
    Console.WriteLine("║       KanbanTracker          ║");
    Console.WriteLine("╠══════════════════════════════╣");
    Console.WriteLine("║ 1. Показати дошку            ║");
    Console.WriteLine("║ 2. Додати завдання           ║");
    Console.WriteLine("║ 3. Перемістити завдання      ║");
    Console.WriteLine("║ 4. Призначити виконавця      ║");
    Console.WriteLine("║ 5. Зберегти у JSON           ║");
    Console.WriteLine("║ 6. Завантажити з JSON        ║");
    Console.WriteLine("║ 0. Вийти                     ║");
    Console.WriteLine("╚══════════════════════════════╝");
    Console.Write("\nОберіть дію: ");

    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            ShowBoard();
            break;
        case "2":
            AddTask();
            break;
        case "3":
            MoveTask();
            break;
        case "4":
            AssignUser();
            break;
        case "5":
            SaveBoard();
            break;
        case "6":
            LoadBoard();
            break;
        case "0":
            Console.WriteLine("До побачення!");
            return;
        default:
            Console.WriteLine("Невірний вибір.");
            break;
    }

    Console.WriteLine("\nНатисніть будь-яку клавішу...");
    Console.ReadKey();
}

// ── Функції ──────────────────────────────────────────────

void ShowBoard()
{
    Console.Clear();
    Console.WriteLine($"\n=== Дошка: {board.Name} ===\n");
    foreach (var col in board.Columns)
    {
        Console.WriteLine($"[ {col.Name} ] ({col.Tasks.Count} завдань)");
        Console.WriteLine(new string('-', 40));
        if (col.Tasks.Count == 0)
        {
            Console.WriteLine("  (порожньо)");
        }
        foreach (var t in col.Tasks)
        {
            var assignee = t.Assignee?.Name ?? "—";
            Console.WriteLine($"  [{t.Priority}] {t.Title}");
            Console.WriteLine($"         Виконавець: {assignee}");
            if (t is BugReport)
                Console.WriteLine($"         [BUG REPORT]");
        }
        Console.WriteLine();
    }
    Console.WriteLine($"Учасники: {string.Join(", ", board.Members.Select(m => m.Name))}");
}

void AddTask()
{
    Console.Clear();
    Console.WriteLine("=== Додати завдання ===\n");

    Console.Write("Назва завдання: ");
    var title = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(title))
    {
        Console.WriteLine("Назва не може бути порожньою.");
        return;
    }

    Console.Write("Опис (можна пропустити): ");
    var desc = Console.ReadLine() ?? "";

    Console.WriteLine("Пріоритет: 1 - Low, 2 - Medium, 3 - High");
    Console.Write("Оберіть: ");
    var pInput = Console.ReadLine();
    var priority = pInput switch
    {
        "1" => Priority.Low,
        "3" => Priority.High,
        _   => Priority.Medium
    };

    Console.WriteLine("Тип: 1 - Звичайне, 2 - Bug Report");
    Console.Write("Оберіть: ");
    var typeInput = Console.ReadLine();

    TaskItem newTask;
    if (typeInput == "2")
    {
        Console.Write("Очікувана поведінка: ");
        var expected = Console.ReadLine() ?? "";
        Console.Write("Фактична поведінка: ");
        var actual = Console.ReadLine() ?? "";
        newTask = new BugReport(title, desc, expected, actual);
    }
    else
    {
        newTask = string.IsNullOrWhiteSpace(desc)
            ? new TaskItem(title, priority)
            : new TaskItem(title, desc, priority);
    }

    todo.AddTask(newTask);
    Console.WriteLine($"\nЗавдання '{title}' додано до колонки Todo.");
}

void MoveTask()
{
    Console.Clear();
    Console.WriteLine("=== Перемістити завдання ===\n");

    // Показуємо всі завдання з номерами
    var allTasks = board.Columns.SelectMany(c => c.Tasks).ToList();
    if (allTasks.Count == 0)
    {
        Console.WriteLine("Немає завдань.");
        return;
    }

    for (int i = 0; i < allTasks.Count; i++)
    {
        var col = board.Columns.First(c => c.Tasks.Contains(allTasks[i]));
        Console.WriteLine($"  {i + 1}. [{allTasks[i].Priority}] {allTasks[i].Title} (зараз: {col.Name})");
    }

    Console.Write("\nОберіть завдання (номер): ");
    if (!int.TryParse(Console.ReadLine(), out int taskIdx) || taskIdx < 1 || taskIdx > allTasks.Count)
    {
        Console.WriteLine("Невірний вибір.");
        return;
    }
    var task = allTasks[taskIdx - 1];
    var fromCol = board.Columns.First(c => c.Tasks.Contains(task));

    // Показуємо колонки
    Console.WriteLine("\nОберіть колонку призначення:");
    var cols = board.Columns.ToList();
    for (int i = 0; i < cols.Count; i++)
        Console.WriteLine($"  {i + 1}. {cols[i].Name}");

    Console.Write("Оберіть колонку (номер): ");
    if (!int.TryParse(Console.ReadLine(), out int colIdx) || colIdx < 1 || colIdx > cols.Count)
    {
        Console.WriteLine("Невірний вибір.");
        return;
    }
    var toCol = cols[colIdx - 1];

    if (fromCol == toCol)
    {
        Console.WriteLine("Завдання вже в цій колонці.");
        return;
    }

    board.MoveTask(task, fromCol, toCol);
    Console.WriteLine($"\nЗавдання '{task.Title}' переміщено: {fromCol.Name} → {toCol.Name}");
}

void AssignUser()
{
    Console.Clear();
    Console.WriteLine("=== Призначити виконавця ===\n");

    var allTasks = board.Columns.SelectMany(c => c.Tasks).ToList();
    if (allTasks.Count == 0)
    {
        Console.WriteLine("Немає завдань.");
        return;
    }

    for (int i = 0; i < allTasks.Count; i++)
        Console.WriteLine($"  {i + 1}. {allTasks[i].Title} | Виконавець: {allTasks[i].Assignee?.Name ?? "—"}");

    Console.Write("\nОберіть завдання (номер): ");
    if (!int.TryParse(Console.ReadLine(), out int taskIdx) || taskIdx < 1 || taskIdx > allTasks.Count)
    {
        Console.WriteLine("Невірний вибір.");
        return;
    }
    var task = allTasks[taskIdx - 1];

    Console.WriteLine("\nОберіть виконавця:");
    var members = board.Members.ToList();
    for (int i = 0; i < members.Count; i++)
        Console.WriteLine($"  {i + 1}. {members[i].Name}");
    Console.WriteLine("  0. Зняти виконавця");

    Console.Write("Оберіть (номер): ");
    if (!int.TryParse(Console.ReadLine(), out int userIdx))
    {
        Console.WriteLine("Невірний вибір.");
        return;
    }

    if (userIdx == 0)
    {
        task.Unassign();
        Console.WriteLine($"Виконавця знято з '{task.Title}'.");
    }
    else if (userIdx >= 1 && userIdx <= members.Count)
    {
        task.Assign(members[userIdx - 1]);
        Console.WriteLine($"'{members[userIdx - 1].Name}' призначений на '{task.Title}'.");
    }
    else
    {
        Console.WriteLine("Невірний вибір.");
    }
}

void SaveBoard()
{
    var service = new BoardService();
    service.SaveToFile(board, "board.json");
    Console.WriteLine("Дошку збережено у board.json");
}

void LoadBoard()
{
    var service = new BoardService();
    var loaded = service.LoadFromFile("board.json");
    if (loaded == null) return;

    Console.WriteLine($"\nЗавантажено: {loaded.Name}");
    foreach (var col in loaded.Columns)
    {
        Console.WriteLine($"  {col.Name} ({col.Tasks.Count} завдань)");
        foreach (var t in col.Tasks)
            Console.WriteLine($"    - [{t.Priority}] {t.Title} | {t.Status}");
    }
}