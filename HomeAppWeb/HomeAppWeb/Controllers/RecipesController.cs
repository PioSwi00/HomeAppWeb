using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeAppWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly UserManager<User> _userManager;

        public RecipesController(IRecipeService recipeService, UserManager<User> userManager)
        {
            _recipeService = recipeService;
            _userManager = userManager;
        }

        // GET: api/Recipes
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes()
        {
            var recipes = await _recipeService.GetAllAsync();
            var recipeDtos = recipes.Select(recipe => new RecipeDto
            {
                RecipeId = recipe.RecipeId,
                Name = recipe.Name,
                RecipeIngredients = recipe.RecipeIngredients.Select(ri => new IngredientDto
                {
                    Name = ri.Ingredient.Name,
                    Quantity = ri.Ingredient.Quantity
                }).ToList(),
                Instructions = recipe.Instructions,
                Author = recipe.Author,
                Rating = recipe.Rating,
                DateAdded = recipe.DateAdded
            }).ToList();

            return Ok(recipeDtos);
        }

        // GET: api/Recipes/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(Guid id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            var recipeDto = new RecipeDto
            {
                RecipeId = recipe.RecipeId,
                Name = recipe.Name,
                RecipeIngredients = recipe.RecipeIngredients.Select(ri => new IngredientDto
                {
                    Name = ri.Ingredient.Name,
                    Quantity = ri.Ingredient.Quantity
                }).ToList(),
                Instructions = recipe.Instructions,
                Author = recipe.Author,
                Rating = recipe.Rating,
                DateAdded = recipe.DateAdded
            };

            return Ok(recipeDto);

        }
        // POST: api/Recipes
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> PostRecipe(CreateRecipeDto createRecipeDto)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User email not found in token.");
            }

            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return Unauthorized("User not found in the database.");
            }

            var userId = user.Id;

            var recipe = new Recipe
            {
                RecipeId = Guid.NewGuid(),
                Name = createRecipeDto.Name,
                Instructions = createRecipeDto.Instructions,
                UserId = userId,
                User = user,
                Rating = null,
                DateAdded = DateTime.UtcNow
            };

            foreach (var ingredientDto in createRecipeDto.Ingredients)
            {
                var ingredient = new Ingredient
                {
                    IngredientId = Guid.NewGuid(),
                    Name = ingredientDto.Name,
                    Quantity = ingredientDto.Quantity
                };

                recipe.RecipeIngredients.Add(new RecipeIngredient
                {
                    RecipeId = recipe.RecipeId,
                    IngredientId = ingredient.IngredientId,
                    Ingredient = ingredient
                });
            }

            await _recipeService.AddAsync(recipe);
            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.RecipeId }, recipe);
        }

        // PUT: api/Recipes/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutRecipe(Guid id, Recipe recipe)
        {
            if (id != recipe.RecipeId)
            {
                return BadRequest();
            }

            await _recipeService.UpdateAsync(recipe);
            return NoContent();
        }

        // DELETE: api/Recipes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            await _recipeService.DeleteAsync(id);
            return NoContent();
        }
    }
    public class CreateRecipeDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public List<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();

        [Required]
        public string Instructions { get; set; }
    }
    public class IngredientDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Quantity { get; set; }
    }
    public class RecipeDto
    {
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
        public List<IngredientDto> RecipeIngredients { get; set; }
        public string Instructions { get; set; }
        public string Author { get; set; }
        public int? Rating { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
