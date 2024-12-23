using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Interfaces.Services
{
    public interface IBillService
    {
        Task<IEnumerable<Bill>> GetAllAsync();
        Task<Bill> GetByIdAsync(Guid id);
        Task AddAsync(Bill bill);
        Task UpdateAsync(Bill bill);
        Task DeleteAsync(Guid id);
    }
}
