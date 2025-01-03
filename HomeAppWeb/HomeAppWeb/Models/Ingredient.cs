using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAppWeb.Models
{
    public class Ingredient
    {
        [Key]
        public Guid IngredientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Quantity { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}


