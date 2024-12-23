using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeAppWeb.Models
{
    [Table("Users")]
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; } // "User" or "Admin"
        public ICollection<UserEvent> UserEvents { get; set; }
        public ICollection<Bill> Bills { get; set; }
        public ICollection<Todo> Todos { get; set; }
        public ICollection<ActionLog> ActionLogs { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}