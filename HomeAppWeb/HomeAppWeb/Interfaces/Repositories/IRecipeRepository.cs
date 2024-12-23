using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Interfaces.Repositories
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<Recipe> GetByIdAsync(Guid id);
        Task AddAsync(Recipe recipe);
        void Update(Recipe recipe);
        void Delete(Recipe recipe);
    }
}
