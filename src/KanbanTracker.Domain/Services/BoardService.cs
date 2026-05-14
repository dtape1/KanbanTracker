using System.Text.Json;
using System.Text.Json.Serialization;
using KanbanTracker.Domain.Models;

namespace KanbanTracker.Domain.Services;

public class BoardService
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };

    public void SaveToFile(Board board, string path)
    {
        var dto = new BoardDto
        {
            Name = board.Name,
            CreatedAt = board.CreatedAt,
            Members = board.Members.Select(u => new UserDto
            {
                Name = u.Name,
                Email = u.Email
            }).ToList(),
            Columns = board.Columns.Select(c => new ColumnDto
            {
                Name = c.Name,
                Tasks = c.Tasks.Select(t => new TaskDto
                {
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority.ToString(),
                    Status = t.Status.ToString(),
                    AssigneeName = t.Assignee?.Name,
                    IsBugReport = t is BugReport
                }).ToList()
            }).ToList()
        };

        var json = JsonSerializer.Serialize(dto, Options);
        File.WriteAllText(path, json);
        Console.WriteLine($"Збережено у файл: {path}");
    }

    public BoardDto? LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не знайдено.");
            return null;
        }
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<BoardDto>(json, Options);
    }
}

// ── DTO класи ───────────────────────────────────────────
public class BoardDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<UserDto> Members { get; set; } = new();
    public List<ColumnDto> Columns { get; set; } = new();
}

public class UserDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class ColumnDto
{
    public string Name { get; set; } = string.Empty;
    public List<TaskDto> Tasks { get; set; } = new();
}

public class TaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? AssigneeName { get; set; }
    public bool IsBugReport { get; set; }
}