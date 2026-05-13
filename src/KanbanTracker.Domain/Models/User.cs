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

    public override string ToString() => $"{Name} ({Email})";
}