using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(string id);
        Task AddAsync(Event eventEntity);
        void Update(Event eventEntity);
        void Delete(Event eventEntity);
    }
}

