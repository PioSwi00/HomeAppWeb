using HomeAppWeb.Data;
using HomeAppWeb.Interfaces.Repositories;
using HomeAppWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Repos
{
    public class UserEventRepository : IUserEventRepository
    {
        private readonly DatabaseContext _context;

        public UserEventRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserEvent>> GetAllAsync()
        {
            return await _context.UserEvents.ToListAsync();
        }

        public async Task<UserEvent> GetByIdAsync(string userId, string eventId)
        {
            return await _context.UserEvents.FindAsync(userId, eventId);
        }

        public async Task AddAsync(UserEvent userEvent)
        {
            await _context.UserEvents.AddAsync(userEvent);
        }

        public void Update(UserEvent userEvent)
        {
            _context.UserEvents.Update(userEvent);
        }

        public void Delete(UserEvent userEvent)
        {
            _context.UserEvents.Remove(userEvent);
        }
    }
}

