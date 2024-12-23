using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Interfaces.Repositories
{
    public interface IUserEventRepository
    {
        Task<IEnumerable<UserEvent>> GetAllAsync();
        Task<UserEvent> GetByIdAsync(string userId, string eventId);
        Task AddAsync(UserEvent userEvent);
        void Update(UserEvent userEvent);
        void Delete(UserEvent userEvent);
    }
}

