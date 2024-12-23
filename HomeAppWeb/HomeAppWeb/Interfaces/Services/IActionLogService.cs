using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Interfaces.Services
{
    public interface IActionLogService
    {
        Task<IEnumerable<ActionLog>> GetAllAsync();
        Task<ActionLog> GetByIdAsync(Guid id);
        Task AddAsync(ActionLog actionLog);
        Task UpdateAsync(ActionLog actionLog);
        Task DeleteAsync(Guid id);
    }
}
