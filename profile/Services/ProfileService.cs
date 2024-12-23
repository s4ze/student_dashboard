using messages.Contracts;
using messages.Data;
using messages.Models;

namespace messages.Services;

public class ProfileService(DataContext context)
{
    private readonly DataContext _context = context;
    public bool CheckIfUserExists(Guid userId)
    {
        return _context.Users.Any(u => u.UserId == userId);
    }
    public User GetUser(Guid userId)
    {
        return _context.Users.First(u => u.UserId == userId);
    }
    public object? CreateUser(CreateUserRequest data)
    {
        if (!_context.Users.Any(u => u.Email == data.Email))
        {
            var user = new User()
            {
                UserId = new Guid(),
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Role = data.Role ?? string.Empty,
                PhotoUrl = data.PhotoUrl ?? string.Empty,
                Contact = data.Contact ?? string.Empty,
                Group = data.Group ?? string.Empty,
                Password = data.Password,
                CreatedAt = DateTime.Now.ToString("MM-dd-yyyy HH:mmK"),
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            var result = new
            {
                user.Email,
                user.FirstName,
                user.LastName,
                user.Role,
                user.PhotoUrl,
                user.Contact,
                user.Group,
                user.CreatedAt
            };
            return result;
        }
        return null;
    }
    public object EditUser(Guid userId, EditUserRequest data)
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
        var result = new
        {
            user.Email,
            user.FirstName,
            user.LastName,
            user.Role,
            user.PhotoUrl,
            user.Contact,
            user.Group,
            user.CreatedAt
        };
        return result;
    }
    public void RemoveUser(Guid userId)
    {
        var user = GetUser(userId);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}
