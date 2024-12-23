using HomeAppWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Interface.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }
}