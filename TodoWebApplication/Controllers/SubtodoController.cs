using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoWebApplication.Dtos;
using TodoWebApplication.Interfaces;
using TodoWebApplication.Models;

namespace TodoWebApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubtodoController : ControllerBase
{
    private readonly ISubtodoRepository _subtodoRepository;

    private readonly IMapper _mapper;

    public SubtodoController(ISubtodoRepository subtodoRepository, IMapper mapper)
    {
        _subtodoRepository = subtodoRepository;

        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllSubtodos()
    {
        var subtodos = _subtodoRepository.GetAllSubtodos();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(subtodos);
    }

    [HttpGet("{subtodoId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetSubtodoById(int subtodoId)
    {
        if (!_subtodoRepository.SubtodoExists(subtodoId))
            return NotFound();

        var subtodo = _subtodoRepository.GetSubtodoById(subtodoId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(subtodo);
    }

    [HttpGet("{subtodoId}/todo")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetTodoOfASubtodo(int subtodoId)
    {
        if (!_subtodoRepository.SubtodoExists(subtodoId))
            return NotFound();

        var todo = _subtodoRepository.GetTodoOfASubtodo(subtodoId);

        if (todo is null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(todo);
    }

    [HttpGet("{subtodoId}/user")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetUserOfASubtodo(int subtodoId)
    {
        if (!_subtodoRepository.SubtodoExists(subtodoId))
            return NotFound();

        var user = _subtodoRepository.GetUserOfASubtodo(subtodoId);

        if (user is null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult CreateSubtodo([FromBody] SubtodoDto subtodoToCreate)
    {
        if (subtodoToCreate is null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var mapToSubtodo = _mapper.Map<Subtodo>(subtodoToCreate);

        _subtodoRepository.CreateSubtodo(mapToSubtodo);

        return NoContent();
    }
    
    [HttpPut("{subtodoId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdateSubtodo([FromBody] SubtodoDto subtodoToUpdate)
    {
        if (!_subtodoRepository.SubtodoExists(subtodoToUpdate.Id))
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var mapToSubtodo = _mapper.Map<Subtodo>(subtodoToUpdate);

        _subtodoRepository.UpdateSubtodo(mapToSubtodo);

        return NoContent();
    }

    [HttpDelete("{subtodoId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteSubtodo(int subtodoId)
    {
        if (!_subtodoRepository.SubtodoExists(subtodoId))
            return NotFound();

        var subtodoToDelete = _subtodoRepository.GetSubtodoById(subtodoId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _subtodoRepository.DeleteSubtodo(subtodoToDelete);

        return NoContent();
    }
}