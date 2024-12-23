using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Interfaces.Services
{
    public interface IUserEventService
    {
        Task<IEnumerable<UserEvent>> GetAllAsync();
        Task<UserEvent> GetByIdAsync(string userId, string eventId);
        Task AddAsync(UserEvent userEvent);
        Task UpdateAsync(UserEvent userEvent);
        Task DeleteAsync(string userId, string eventId);
    }
}
