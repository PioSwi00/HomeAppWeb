using HomeAppWeb.Data;
using HomeAppWeb.Interfaces.Repositories;
using HomeAppWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Repos
{
    public class ActionLogRepository : IActionLogRepository
    {
        private readonly DatabaseContext _context;

        public ActionLogRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActionLog>> GetAllAsync()
        {
            return await _context.ActionLogs.ToListAsync();
        }

        public async Task<ActionLog> GetByIdAsync(Guid id)
        {
            return await _context.ActionLogs.FindAsync(id);
        }

        public async Task AddAsync(ActionLog actionLog)
        {
            await _context.ActionLogs.AddAsync(actionLog);
        }

        public void Update(ActionLog actionLog)
        {
            _context.ActionLogs.Update(actionLog);
        }

        public void Delete(ActionLog actionLog)
        {
            _context.ActionLogs.Remove(actionLog);
        }
    }
}

