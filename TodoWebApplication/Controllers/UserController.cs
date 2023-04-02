using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoWebApplication.Dtos;
using TodoWebApplication.Interfaces;
using TodoWebApplication.Models;

namespace TodoWebApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;

        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllUsers()
    {
        var users = _userRepository.GetAllUsers();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(users);
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetUserById(int userId)
    {
        if (!_userRepository.UserExists(userId))
            return NotFound();

        var user = _mapper.Map<UserDto>(_userRepository.GetUserById(userId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(user);
    }
    
    [HttpGet("{userId}/todos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetTodosOfAUser(int userId)
    {
        if (!_userRepository.UserExists(userId))
            return NotFound();
            
        var todos = _userRepository.GetTodosOfAUser(userId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(todos);
    }
    
    [HttpGet("{userId}/subtodos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetSubtodosOfAUser(int userId)
    {
        if (!_userRepository.UserExists(userId))
            return NotFound();
        
        var subtodos = _userRepository.GetSubtodosOfAUser(userId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(subtodos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateUser([FromBody] UserDto userToCreate)
    {
        if (userToCreate is null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var mapToUser = _mapper.Map<User>(userToCreate);

        _userRepository.CreateUser(mapToUser);

        return NoContent();
    }

    [HttpPut("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateUser([FromBody] UserDto userToUpdate)
    {
        if (!_userRepository.UserExists(userToUpdate.Id))
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (userToUpdate is null)
            return NotFound();
        
        var mapToUser = _mapper.Map<User>(userToUpdate);

        _userRepository.UpdateUser(mapToUser);

        return NoContent();
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteUser(int userId)
    {
        if (!_userRepository.UserExists(userId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userToDelete = _userRepository.GetUserById(userId);

        if (userToDelete is null)
            return BadRequest(ModelState);

        _userRepository.DeleteUser(userToDelete);
        
        return NoContent();
    }
}