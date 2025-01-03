using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Interfaces.Services
{
    public interface IToDoService
    {
        Task<IEnumerable<ToDo>> GetUserToDosAsync(string userId);
        Task<ToDo> GetToDoDetailsAsync(Guid id);
        Task CreateToDoAsync(ToDo toDo, List<string> assignedUserIds);
        Task UpdateToDoStatusAsync(Guid id, ToDoStatus status, string userId);
        Task DeleteToDoAsync(Guid id, string userId);
    }
}



