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
            return await _context.Recipes
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<Recipe> GetByIdAsync(Guid id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.RecipeId == id);

            if (recipe == null)
            {
                throw new KeyNotFoundException($"Recipe with ID {id} not found.");
            }

            return recipe;
        }

        public async Task AddAsync(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }
    }
}

