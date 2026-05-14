using KanbanTracker.Domain.Enums;
using KanbanTracker.Domain.Models;
using KanbanTracker.Domain.Patterns.State;
using KanbanTracker.Domain.Patterns.Factory;
using KanbanTracker.Domain.Patterns.Composite;

// ── Патерн State ────────────────────────────────────────
Console.WriteLine("=== Патерн State ===");
var stateManager = new TaskStateManager();

var task1 = new TaskItem("Написати тести", Priority.High);
Console.WriteLine($"Початковий стан: {stateManager.GetStateName(task1)}");
stateManager.MoveNext(task1);  // Todo → InProgress
stateManager.MoveNext(task1);  // InProgress → Review
stateManager.MoveNext(task1);  // Review → Done
stateManager.MoveNext(task1);  // Done → вже кінець
stateManager.MovePrev(task1);  // Done → Review

// ── Патерн Factory ──────────────────────────────────────
Console.WriteLine("\n=== Патерн Factory ===");

ITaskFactory regularFactory = new RegularTaskFactory(Priority.Medium);
ITaskFactory bugFactory = new BugReportFactory(
    "Дані зберігаються",
    "Дані губляться після перезапуску"
);

var task2 = regularFactory.Create("Додати пагінацію", "Розбити список на сторінки");
var task3 = regularFactory.Create("Рефакторинг сервісу", "Прибрати дублювання коду");
var bug1  = bugFactory.Create("Дані не зберігаються", "Кроки: запустити → закрити → відкрити");

Console.WriteLine($"Створено: {task2}");
Console.WriteLine($"Створено: {task3}");
Console.WriteLine($"Створено: {bug1}");

// ── Патерн Composite ────────────────────────────────────
Console.WriteLine("\n=== Патерн Composite ===");

var epic = new TaskComposite("Епік: Авторизація");

var feature1 = new TaskComposite("Фіча: Логін");
feature1.Add(new TaskLeaf(new TaskItem("Форма логіну", Priority.High)));
feature1.Add(new TaskLeaf(new TaskItem("Валідація полів", Priority.Medium)));
feature1.Add(new TaskLeaf(new TaskItem("JWT токен", Priority.High)));

var feature2 = new TaskComposite("Фіча: Реєстрація");
feature2.Add(new TaskLeaf(new TaskItem("Форма реєстрації", Priority.Medium)));
feature2.Add(new TaskLeaf(new TaskItem("Відправка email", Priority.Low)));

epic.Add(feature1);
epic.Add(feature2);
epic.Add(new TaskLeaf(bug1));

epic.Display();
Console.WriteLine($"\nВсього завдань в епіку: {epic.GetTotalCount()}");