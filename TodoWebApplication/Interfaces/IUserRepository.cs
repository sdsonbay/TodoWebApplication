using TodoWebApplication.Models;

namespace TodoWebApplication.Interfaces;

public interface IUserRepository
{
    ICollection<User> GetAllUsers();
    User GetUserById(int userId);
    ICollection<Todo> GetTodosOfAUser(int userId);
    ICollection<Subtodo> GetSubtodosOfAUser(int userId);
    bool UserExists(int userId);
    bool CreateUser(User user);
    bool UpdateUser(User user);
    bool DeleteUser(User user);
    bool Save();
}