using HomeAppWeb.Interfaces;
using HomeAppWeb.Interfaces.Repositories;
using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppWeb.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToDoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ToDo>> GetUserToDosAsync(string userId)
        {
            var toDos = await _unitOfWork.ToDos.GetAllToDosAsync();
            return toDos.Where(t => t.CreatorId == userId || t.AssignedUsers.Any(u => u.UserId == userId));
        }

        public async Task<ToDo> GetToDoDetailsAsync(Guid id)
        {
            return await _unitOfWork.ToDos.GetToDoByIdAsync(id);
        }

        public async Task CreateToDoAsync(ToDo toDo, List<string> assignedUserIds)
        {
            // Sprawdź, czy CreatorId istnieje
            var creator = await _unitOfWork.Users.GetByIdAsync(toDo.CreatorId);
            if (creator == null)
            {
                throw new InvalidOperationException("CreatorId does not exist.");
            }

            // Generowanie nowego ToDoId
            toDo.ToDoId = Guid.NewGuid();

            // Przypisywanie użytkowników
            toDo.AssignedUsers = assignedUserIds.Select(userId => new ToDoUser { UserId = userId, ToDoId = toDo.ToDoId }).ToList();

            await _unitOfWork.ToDos.AddToDoAsync(toDo);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateToDoStatusAsync(Guid id, ToDoStatus status, string userId)
        {
            var toDo = await _unitOfWork.ToDos.GetToDoByIdAsync(id);
            if (toDo == null || (toDo.CreatorId != userId && !toDo.AssignedUsers.Any(u => u.UserId == userId)))
            {
                throw new UnauthorizedAccessException("You do not have permission to update this ToDo.");
            }

            toDo.Status = status;
            await _unitOfWork.ToDos.UpdateToDoAsync(toDo);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteToDoAsync(Guid id, string userId)
        {
            var toDo = await _unitOfWork.ToDos.GetToDoByIdAsync(id);
            if (toDo == null || toDo.CreatorId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this ToDo.");
            }

            await _unitOfWork.ToDos.DeleteToDoAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}