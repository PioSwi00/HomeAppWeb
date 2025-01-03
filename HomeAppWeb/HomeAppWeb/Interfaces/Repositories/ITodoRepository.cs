using HomeAppWeb.Models;

namespace HomeAppWeb.Interfaces.Repositories
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDo>> GetAllToDosAsync();
        Task<ToDo> GetToDoByIdAsync(Guid id);
        Task AddToDoAsync(ToDo toDo);
        Task UpdateToDoAsync(ToDo toDo);
        Task DeleteToDoAsync(Guid id);
    }
}
            