using HomeAppWeb.Data;
using HomeAppWeb.Interfaces.Repositories;
using HomeAppWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Repos
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly DatabaseContext _context;

        public TodoItemRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync(Guid id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task AddAsync(TodoItem todoItem)
        {
            await _context.TodoItems.AddAsync(todoItem);
        }

        public void Update(TodoItem todoItem)
        {
            _context.TodoItems.Update(todoItem);
        }

        public void Delete(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
        }
    }
}

