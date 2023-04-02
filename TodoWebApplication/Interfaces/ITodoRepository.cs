using TodoWebApplication.Models;

namespace TodoWebApplication.Interfaces;

public interface ITodoRepository
{
    ICollection<Todo> GetAllTodos();
    ICollection<Todo> GetAllTodosOrderByLastUpdatedDate();
    Todo GetTodoById(int todoId);
    ICollection<Todo> GetDoneTodos();
    ICollection<Todo> GetUndoneTodos();
    ICollection<Todo> GetTodosOverProgress(int progress);
    ICollection<Todo> GetTodosBelowProgress(int progress);
    User GetUserOfATodo(int todoId);
    ICollection<Subtodo> GetSubtodosOfATodo(int todoId);
    bool TodoExists(int todoId);
    bool CreateTodo(Todo todo);
    bool UpdateTodo(Todo todo);
    bool DeleteTodo(Todo todo);
    bool Save();
}