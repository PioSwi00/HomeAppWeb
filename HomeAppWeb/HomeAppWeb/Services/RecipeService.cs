using HomeAppWeb.Interfaces;
using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAppWeb.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await _unitOfWork.Recipes.GetAllAsync();
        }

        public async Task<Recipe> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Recipes.GetByIdAsync(id);
        }

        public async Task AddAsync(Recipe recipe)
        {
            await _unitOfWork.Recipes.AddAsync(recipe);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            _unitOfWork.Recipes.UpdateAsync(recipe);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(id);
            if (recipe != null)
            {
                _unitOfWork.Recipes.DeleteAsync(recipe);
                await _unitOfWork.CompleteAsync();
            }
        }

        

        

     
    }
}

