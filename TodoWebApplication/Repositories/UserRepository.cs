using TodoWebApplication.Data;
using TodoWebApplication.Interfaces;
using TodoWebApplication.Models;

namespace TodoWebApplication.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public ICollection<User> GetAllUsers()
    {
        return _context.Users.OrderBy(u => u.Id).ToList();
    }

    public User GetUserById(int userId)
    {
        return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
    }

    public ICollection<Todo> GetTodosOfAUser(int userId)
    {
        return _context.Todos.Where(t => t.UserId == userId).ToList();
    }

    public ICollection<Subtodo> GetSubtodosOfAUser(int userId)
    {
        return _context.Subtodos.Where(s => s.Todo.UserId == userId).ToList();
    }

    public bool UserExists(int userId)
    {
        return _context.Users.Any(u => u.Id == userId);
    }

    public bool CreateUser(User user)
    {
        _context.Users.Add(user);
        
        return Save();
    }

    public bool UpdateUser(User user)
    {
        _context.Users.Update(user);
        
        return Save();
    }

    public bool DeleteUser(User user)
    {
        _context.Users.Remove(user);
        
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();

        return saved > 0 ? true : false;
    }
}