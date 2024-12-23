using messages.Contracts;
using messages.Data;
using messages.Models;

namespace messages.Services;

public class ProfileService(DataContext context)
{
    private readonly DataContext _context = context;
    private static readonly HttpClient client = new();
    public bool CheckIfUserExists(Guid userId)
    {
        return _context.Users.Any(u => u.UserId == userId);
    }
    public User GetUser(Guid userId)
    {
        return _context.Users.First(u => u.UserId == userId);
    }
    public User EditUser(Guid userId, EditUserRequest data)
    {
        var user = GetUser(userId);
        user.Email = data.Email ?? user.Email;
        user.FirstName = data.FirstName ?? user.FirstName;
        user.LastName = data.LastName ?? user.LastName;
        user.PhotoUrl = data.PhotoUrl ?? user.PhotoUrl;
        user.Contact = data.Contact ?? user.Contact;
        user.Group = data.Group ?? user.Group;
        user.Password = data.Password ?? user.Password;
        _context.SaveChanges();
        return user;
    }
    public void RemoveUser(Guid userId)
    {
        var user = GetUser(userId);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}
