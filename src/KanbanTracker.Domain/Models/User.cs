using KanbanTracker.Domain.Base;

namespace KanbanTracker.Domain.Models;

public class User : BaseEntity
{
    private string _name;
    private string _email;

    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ім'я не може бути порожнім.");
            _name = value;
        }
    }

    public string Email
    {
        get => _email;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email не може бути порожнім.");
            _email = value;
        }
    }

    public User(string name, string email)
    {
        Name = name;
        Email = email;
    }

    // Перевантаження операторів
    public static bool operator ==(User? a, User? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Id == b.Id;
    }

    public static bool operator !=(User? a, User? b) => !(a == b);

    public override bool Equals(object? obj) =>
        obj is User other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public override string GetSummary() => $"Користувач: {Name} ({Email})";

    public override string ToString() => $"{Name} ({Email})";
}