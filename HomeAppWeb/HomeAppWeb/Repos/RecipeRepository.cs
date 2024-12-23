using HomeAppWeb.Data;
using HomeAppWeb.Interfaces.Repositories;
using HomeAppWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Repos
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DatabaseContext _context;

        public RecipeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<Recipe> GetByIdAsync(Guid id)
        {
            return await _context.Recipes.FindAsync(id);
        }

        public async Task AddAsync(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
        }

        public void Update(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
        }

        public void Delete(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
        }
    }
}

