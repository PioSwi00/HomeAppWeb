using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Interfaces.Repositories
{
    public interface IBillRepository
    {
        Task<IEnumerable<Bill>> GetAllAsync();
        Task<Bill> GetByIdAsync(Guid id);
        Task AddAsync(Bill bill);
        void Update(Bill bill);
        void Delete(Bill bill);
    }
}

