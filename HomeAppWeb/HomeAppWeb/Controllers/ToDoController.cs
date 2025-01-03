using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using HomeAppWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IToDoService _toDoService;
    private readonly UserManager<User> _userManager;

    public ToDoController(IToDoService toDoService, UserManager<User> userManager)
    {
        _toDoService = toDoService;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<IEnumerable<ToDo>>> GetUserToDos()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var toDos = await _toDoService.GetUserToDosAsync(userId);
        return Ok(toDos);
    }
    // GET: api/ToDo/{id}
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<ToDo>> GetToDo(Guid id)
    {
        var toDo = await _toDoService.GetToDoDetailsAsync(id);

        if (toDo == null)
        {
            return NotFound();
        }

        return Ok(toDo);
    }
    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult> CreateToDo([FromBody] CreateToDoDto createToDoDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var toDo = new ToDo
        {
            ToDoId = Guid.NewGuid(),
            Title = createToDoDto.Title,
            Description = createToDoDto.Description,
            DueDate = createToDoDto.DueDate,
            Status = ToDoStatus.Pending,
            CreatorId = userId
        };

        await _toDoService.CreateToDoAsync(toDo, createToDoDto.AssignedUserIds);
        return CreatedAtAction(nameof(GetToDo), new { id = toDo.ToDoId }, toDo);
    }

    public class CreateToDoDto
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public List<string> AssignedUserIds { get; set; } = new List<string>();
    }

}
