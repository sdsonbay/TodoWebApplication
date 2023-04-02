using TodoWebApplication.Data;
using TodoWebApplication.Interfaces;
using TodoWebApplication.Models;

namespace TodoWebApplication.Repositories;

public class SubtodoRepository : ISubtodoRepository
{
    private readonly ApplicationDbContext _context;

    public SubtodoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public ICollection<Subtodo> GetAllSubtodos()
    {
        return _context.Subtodos.OrderBy(s => s.Id).ToList();
    }

    public ICollection<Subtodo> GetAllSubtodosOrderByLastUpdatedDate()
    {
        return _context.Subtodos.OrderBy(s => s.LastUpdatedDate).ToList();
    }

    public Subtodo GetSubtodoById(int subtodoId)
    {
        return _context.Subtodos.Where(s => s.Id == subtodoId).FirstOrDefault();
    }

    public ICollection<Subtodo> GetDoneSubtodos()
    {
        return _context.Subtodos.Where(s => s.IsDone == true).ToList();
    }

    public ICollection<Subtodo> GetUndoneSubtodos()
    {
        return _context.Subtodos.Where(s => s.IsDone == false).ToList();
    }

    public Todo GetTodoOfASubtodo(int subtodoId)
    {
        return _context.Todos.Where(t => t.Subtodos.Any(s => s.Id == subtodoId)).FirstOrDefault();
    }

    public User GetUserOfASubtodo(int subtodoId)
    {
        var subtodo = _context.Subtodos.Where(s => s.Id == subtodoId).FirstOrDefault();
        var todo = _context.Todos.Where(t => t.Subtodos.Contains(subtodo)).FirstOrDefault();
        var user = _context.Users.Where(u => u.Todos.Contains(todo)).FirstOrDefault();

        return user;
    }

    public bool SubtodoExists(int subtodoId)
    {
        return _context.Subtodos.Any(s => s.Id == subtodoId);
    }

    public bool CreateSubtodo(Subtodo subtodo)
    {
        _context.Subtodos.Add(subtodo);

        return Save();
    }

    public bool UpdateSubtodo(Subtodo subtodo)
    {
        _context.Subtodos.Update(subtodo);

        return Save();
    }

    public bool DeleteSubtodo(Subtodo subtodo)
    {
        _context.Subtodos.Remove(subtodo);

        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();

        return saved > 0 ? true : false;
    }
}