using HomeAppWeb.Data;
using HomeAppWeb.Interfaces.Repositories;
using HomeAppWeb.Models;
using Microsoft.EntityFrameworkCore;

public class ToDoRepository : IToDoRepository
{
    private readonly DatabaseContext _context;

    public ToDoRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ToDo>> GetAllToDosAsync()
    {
        return await _context.ToDos.Include(t => t.AssignedUsers).ThenInclude(tu => tu.User).ToListAsync();
    }

    public async Task<ToDo> GetToDoByIdAsync(Guid id)
    {
        return await _context.ToDos.Include(t => t.AssignedUsers).ThenInclude(tu => tu.User).FirstOrDefaultAsync(t => t.ToDoId == id);
    }

    public async Task AddToDoAsync(ToDo toDo)
    {
        await _context.ToDos.AddAsync(toDo);
    }

    public async Task UpdateToDoAsync(ToDo toDo)
    {
        _context.ToDos.Update(toDo);
    }

    public async Task DeleteToDoAsync(Guid id)
    {   
        var toDo = await GetToDoByIdAsync(id);
        if (toDo != null)
        {
            _context.ToDos.Remove(toDo);
        }
    }
}
