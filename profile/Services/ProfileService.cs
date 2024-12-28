using profile.Contracts;
using profile.Data;
using profile.Models;

namespace profile.Services;

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
    public bool CreateUser(RegisterRequest data)
    {
        if (!_context.Users.Any(u => u.Email == data.Email))
        {
            var user = new User()
            {
                UserId = new Guid(),
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Role = "student",
                PhotoUrl = data.PhotoUrl ?? string.Empty,
                Contact = data.Contact ?? string.Empty,
                Group = data.Group ?? string.Empty,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password, BCrypt.Net.BCrypt.GenerateSalt(8)),
                CreatedAt = DateTime.Now.ToString("MM-dd-yyyy HH:mmK"),
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }
        return false;
    }
    public UserResponse EditUser(Guid userId, EditUserRequest data)
    {
        var user = GetUser(userId);
        user.Email = data.Email ?? user.Email;
        user.FirstName = data.FirstName ?? user.FirstName;
        user.LastName = data.LastName ?? user.LastName;
        user.PhotoUrl = data.PhotoUrl ?? user.PhotoUrl;
        user.Contact = data.Contact ?? user.Contact;
        user.Group = data.Group ?? user.Group;
        user.Password = BCrypt.Net.BCrypt.HashPassword(data.Password, BCrypt.Net.BCrypt.GenerateSalt(8));
        _context.SaveChanges();

        return new UserResponse()
        {
            UserId = user.UserId.ToString(),
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhotoUrl = user.PhotoUrl,
            Contact = user.Contact,
            Group = user.Group,
            CreatedAt = user.CreatedAt
        };
    }
    public void RemoveUser(Guid userId)
    {
        var user = GetUser(userId);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}
