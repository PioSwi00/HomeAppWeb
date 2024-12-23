using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAppWeb.Models
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

          
        public DateOnly BirthDate { get; set; }
              
        public DateOnly? DeathDate { get; set; }

        [Required]
        public string UserId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        [Required]
        public User User { get; set; }
    }

    
}
