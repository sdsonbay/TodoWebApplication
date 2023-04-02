using TodoWebApplication.Data;
using TodoWebApplication.Interfaces;
using TodoWebApplication.Models;

namespace TodoWebApplication.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDbContext _context;
    
    public TodoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public ICollection<Todo> GetAllTodos()
    {
        return _context.Todos.OrderBy(t => t.Id).ToList();
    }

    public ICollection<Todo> GetAllTodosOrderByLastUpdatedDate()
    {
        return _context.Todos.OrderBy(t => t.LastUpdatedDate).ToList();
    }

    public Todo GetTodoById(int todoId)
    {
        return _context.Todos.Where(t => t.Id == todoId).FirstOrDefault();
    }

    public ICollection<Todo> GetDoneTodos()
    {
        return _context.Todos.Where(t => t.IsDone == true).ToList();
    }

    public ICollection<Todo> GetUndoneTodos()
    {
        return _context.Todos.Where(t => t.IsDone == false).ToList();
    }

    public ICollection<Todo> GetTodosOverProgress(int progress)
    {
        return _context.Todos.Where(t => t.Progress >= progress).ToList();
    }

    public ICollection<Todo> GetTodosBelowProgress(int progress)
    {
        return _context.Todos.Where(t => t.Progress <= progress).ToList();
    }

    public User GetUserOfATodo(int todoId)
    {
        return _context.Users.Where(u => u.Todos.Any(t => t.Id == todoId)).FirstOrDefault();
    }

    public ICollection<Subtodo> GetSubtodosOfATodo(int todoId)
    {
        var subtodos = _context.Subtodos.Where(s => s.TodoId.Equals(todoId)).ToList();
        return subtodos;
    }

    public bool TodoExists(int todoId)
    {
        return _context.Todos.Any(t => t.Id == todoId);
    }

    public bool CreateTodo(Todo todo)
    {
        _context.Todos.Add(todo);

        return Save();
    }

    public bool UpdateTodo(Todo todo)
    {
        _context.Todos.Update(todo);

        return Save();
    }

    public bool DeleteTodo(Todo todo)
    {
        _context.Todos.Remove(todo);

        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();

        return saved > 0 ? true : false;
    }
}