using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAppWeb.Models
{
    [Table("Recipes")]
    public class Recipe
    {
        [Key]
        public Guid RecipeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

        [Required]
        public string Instructions { get; set; }

        [Required]
        public string UserId { get; set; }

        [NotMapped]
        public string Author => $"{User?.FirstName} {User?.LastName}";

        [Range(0, 5)]
        public int? Rating { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
