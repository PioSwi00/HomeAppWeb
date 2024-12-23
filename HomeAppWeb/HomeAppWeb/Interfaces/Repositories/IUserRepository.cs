using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task AddAsync(User user);
        void Update(User user);
        void Delete(User user);
    }
}
