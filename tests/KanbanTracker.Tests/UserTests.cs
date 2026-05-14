using KanbanTracker.Domain.Models;

namespace KanbanTracker.Tests;

public class UserTests
{
    [Fact]
    public void User_CreatedWithCorrectData()
    {
        var user = new User("Давид", "david@test.com");
        Assert.Equal("Давид", user.Name);
        Assert.Equal("david@test.com", user.Email);
    }

    [Fact]
    public void User_EmptyName_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            new User("", "email@test.com"));
    }

    [Fact]
    public void User_EmptyEmail_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            new User("Давид", ""));
    }

    [Fact]
    public void User_TwoUsers_HaveDifferentIds()
    {
        var u1 = new User("Давид", "d@test.com");
        var u2 = new User("Анна", "a@test.com");
        Assert.NotEqual(u1.Id, u2.Id);
    }
}