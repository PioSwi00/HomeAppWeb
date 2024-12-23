using HomeAppWeb.Interfaces;
using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Services
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            return await _unitOfWork.Todos.GetAllAsync();
        }

        public async Task<Todo> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Todos.GetByIdAsync(id);
        }

        public async Task AddAsync(Todo todo)
        {
            await _unitOfWork.Todos.AddAsync(todo);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Todo todo)
        {
            _unitOfWork.Todos.Update(todo);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var todo = await _unitOfWork.Todos.GetByIdAsync(id);
            if (todo != null)
            {
                _unitOfWork.Todos.Delete(todo);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
