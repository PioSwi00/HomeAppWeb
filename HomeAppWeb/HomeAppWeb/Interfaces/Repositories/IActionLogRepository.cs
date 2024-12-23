using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Interfaces.Repositories
{
    public interface IActionLogRepository
    {
        Task<IEnumerable<ActionLog>> GetAllAsync();
        Task<ActionLog> GetByIdAsync(Guid id);
        Task AddAsync(ActionLog actionLog);
        void Update(ActionLog actionLog);
        void Delete(ActionLog actionLog);
    }
}

