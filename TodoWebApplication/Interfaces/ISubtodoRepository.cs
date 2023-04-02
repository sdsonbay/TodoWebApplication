using TodoWebApplication.Models;

namespace TodoWebApplication.Interfaces;

public interface ISubtodoRepository
{
    ICollection<Subtodo> GetAllSubtodos();
    ICollection<Subtodo> GetAllSubtodosOrderByLastUpdatedDate();
    Subtodo GetSubtodoById(int subtodoId);
    ICollection<Subtodo> GetDoneSubtodos();
    ICollection<Subtodo> GetUndoneSubtodos();
    Todo GetTodoOfASubtodo(int subtodoId);
    User GetUserOfASubtodo(int subtodoId);
    bool SubtodoExists(int subtodoId);
    bool CreateSubtodo(Subtodo subtodo);
    bool UpdateSubtodo(Subtodo subtodo);
    bool DeleteSubtodo(Subtodo subtodo);
    bool Save();
}