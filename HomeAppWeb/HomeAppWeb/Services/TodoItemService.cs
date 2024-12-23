using HomeAppWeb.Interface.Services;
using HomeAppWeb.Interfaces;
using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _unitOfWork.TodoItems.GetAllAsync();
        }

        public async Task<TodoItem> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.TodoItems.GetByIdAsync(id);
        }

        public async Task AddAsync(TodoItem todoItem)
        {
            await _unitOfWork.TodoItems.AddAsync(todoItem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(TodoItem todoItem)
        {
            _unitOfWork.TodoItems.Update(todoItem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var todoItem = await _unitOfWork.TodoItems.GetByIdAsync(id);
            if (todoItem != null)
            {
                _unitOfWork.TodoItems.Delete(todoItem);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}

