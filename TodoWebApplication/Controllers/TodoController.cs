using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoWebApplication.Dtos;
using TodoWebApplication.Interfaces;
using TodoWebApplication.Models;

namespace TodoWebApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public TodoController(ITodoRepository todoRepository, IUserRepository userRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllTodos()
    {
        var todos = _todoRepository.GetAllTodos();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(todos);
    }

    [HttpGet("{todoId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetTodoById(int todoId)
    {
        if (!_todoRepository.TodoExists(todoId))
            return NotFound();

        var todo = _mapper.Map<TodoDto>(_todoRepository.GetTodoById(todoId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(todo);
    }
    
    [HttpGet("{todoId}/user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetUserOfATodo(int todoId)
    {
        if (!_todoRepository.TodoExists(todoId))
            return NotFound();
        
        var user = _todoRepository.GetUserOfATodo(todoId);

        if (user is null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(user);
    }
    
    [HttpGet("{todoId}/subtodos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetSubtodosOfATodo(int todoId)
    {
        if (!_todoRepository.TodoExists(todoId))
            return NotFound();

        var subtodos = _todoRepository.GetSubtodosOfATodo(todoId).ToList();

        if (subtodos is null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(subtodos);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateTodo([FromBody] TodoDto todoToCreate)
    {
        if (todoToCreate is null || !_userRepository.UserExists(todoToCreate.UserId))
            return BadRequest(ModelState);

        var user = _userRepository.GetUserById(todoToCreate.UserId);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var mapToTodo = _mapper.Map<Todo>(todoToCreate);

        mapToTodo.User = user;

        _todoRepository.CreateTodo(mapToTodo);

        user.Todos.Add(mapToTodo);
        
        _userRepository.UpdateUser(user);

        return NoContent();
    }
    
    [HttpPut("{todoId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateTodo([FromBody] TodoDto todoToUpdate)
    {
        if (!_todoRepository.TodoExists(todoToUpdate.Id))
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var mapToTodo = _mapper.Map<Todo>(todoToUpdate);

        if (todoToUpdate is null)
            return NotFound();
        
        _todoRepository.UpdateTodo(mapToTodo);

        return NoContent();
    }

    [HttpDelete("{todoId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteTodo(int todoId)
    {
        if (!_todoRepository.TodoExists(todoId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var todoToDelete = _todoRepository.GetTodoById(todoId);

        if (todoToDelete is null)
            return NotFound();

        _todoRepository.DeleteTodo(todoToDelete);

        return NoContent();
    }
}