using authorization.Contracts;
using authorization.Data;
using authorization.Models;

namespace authorization.Services;

public class AuthenticationService(DataContext context)
{
    private readonly DataContext _context = context;
    public bool Login(LoginRequest data)
    {
        if (CheckIfUserExistsByEmail(data.Email))
        {
            var user = _context.Users.First(u => u.Email == data.Email);
            return BCrypt.Net.BCrypt.Verify(data.Password, user.Password);
        }
        return false;
    }
    public bool CheckIfUserExistsById(Guid userId)
    {
        return _context.Users.Any(u => u.UserId == userId);
    }
    public bool CheckIfUserExistsByEmail(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }
    public User GetUserById(Guid userId)
    {
        return _context.Users.First(u => u.UserId == userId);
    }
    public User GetUserByEmail(string email)
    {
        return _context.Users.First(u => u.Email == email);
    }
}
